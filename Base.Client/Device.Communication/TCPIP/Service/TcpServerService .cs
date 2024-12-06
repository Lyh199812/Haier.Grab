using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Device.Events.TCPIP;
using Base.Client.Entity;
using System.Collections.Concurrent;

namespace Device.Communication.TCPIP
{
    public class TcpServerService : INetworkService
    {
        private readonly IEventAggregator _eventAggregator;
        private TcpListener _listener;
        private readonly ConcurrentBag<TcpClient> _connectedClients = new();
        private readonly ConcurrentDictionary<string, TcpClient> _clientDictionary = new();
        private CancellationTokenSource _cts;

        public event EventHandler<DataReceivedEventArgs> OnDataReceived; // 数据接收事件
        public event EventHandler<ClientEventArgs> OnClientConnected;   // 客户端连接事件
        public event EventHandler<ClientEventArgs> OnClientDisconnected; // 客户端断开事件
        public event EventHandler<ClientEventArgs> OnClientDisconnectedManually; // 主动断开客户端事件

        public TcpServerService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// 启动TCP服务器
        /// </summary>
        public async Task<OperateResult> StartAsync(string ipAddress, int port)
        {
            try
            {
                _cts = new CancellationTokenSource();
                _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _listener.Start();
                Console.WriteLine($"TCP Server started on {ipAddress}:{port}");

                _ = Task.Run(async () =>
                {
                    while (!_cts.Token.IsCancellationRequested)
                    {
                        try
                        {
                            var client = await _listener.AcceptTcpClientAsync();
                            HandleNewClient(client);
                        }
                        catch (ObjectDisposedException)
                        {
                            // 监听器已停止
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error accepting client: {ex.ToString()}");
                        }
                    }
                }, _cts.Token);

                return new OperateResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString() };
            }
        }

        /// <summary>
        /// 停止TCP服务器
        /// </summary>
        public async Task<OperateResult> StopAsync()
        {
            try
            {
                _cts?.Cancel();
                _listener?.Stop();

                foreach (var client in _connectedClients)
                {
                    client.Close();
                }

                _connectedClients.Clear();
                _clientDictionary.Clear();

                Console.WriteLine("TCP Server stopped.");
                return new OperateResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString() };
            }
        }

        /// <summary>
        /// 主动断开客户端
        /// </summary>
        public async Task<OperateResult> DisconnectClientAsync(string clientId)
        {
            if (_clientDictionary.TryRemove(clientId, out var client))
            {
                try
                {
                    client.Close();

                    // 通知其他订阅者
                    var clientEventArgs = new ClientEventArgs(clientId);
                    InvokeSafe(() => OnClientDisconnectedManually?.Invoke(this, clientEventArgs));
                    _eventAggregator.GetEvent<ClientDisconnectedManuallyEvent>().Publish(clientEventArgs);

                    Console.WriteLine($"Client {clientId} disconnected manually.");
                    return new OperateResult { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new OperateResult { IsSuccess = false, Message = ex.ToString() };
                }
            }

            Console.WriteLine($"Client {clientId} not found.");
            return new OperateResult { IsSuccess = false, Message = "Client not found." };
        }

        /// <summary>
        /// 处理新客户端连接
        /// </summary>
        private void HandleNewClient(TcpClient client)
        {
            var clientId = GetClientId(client);
            _connectedClients.Add(client);
            _clientDictionary[clientId] = client;

            // 触发客户端连接事件
            var clientEventArgs = new ClientEventArgs(clientId);
            InvokeSafe(() => OnClientConnected?.Invoke(this, clientEventArgs));
            _eventAggregator.GetEvent<ClientConnectedEvent>().Publish(clientEventArgs);

            Console.WriteLine($"Client {clientId} connected.");
            _ = Task.Run(() => HandleClientAsync(client, clientId, _cts.Token));
        }

        /// <summary>
        /// 处理客户端通信
        /// </summary>
        private async Task HandleClientAsync(TcpClient client, string clientId, CancellationToken cancellationToken)
        {
            var stream = client.GetStream();
            var buffer = new byte[1024];

            try
            {
                while (!cancellationToken.IsCancellationRequested && client.Connected)
                {
                    int bytesRead = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken);
                    if (bytesRead > 0)
                    {
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received from {clientId}: {receivedMessage}");

                        // 触发数据接收事件
                        var dataArgs = new DataReceivedEventArgs(receivedMessage, clientId);
                        InvokeSafe(() => OnDataReceived?.Invoke(this, dataArgs));
                        _eventAggregator.GetEvent<DeviceDataReceivedEvent>().Publish(dataArgs);
                        _eventAggregator.GetEvent<DeviceDataReceivedEvent>().Publish(dataArgs);
                    }
                    else
                    {
                        // 客户端断开连接
                        break;
                    }
                }
            }
            catch (Exception ex) when (ex is IOException or OperationCanceledException)
            {
                Console.WriteLine($"Client {clientId} disconnected unexpectedly: {ex.ToString()}");
            }
            finally
            {
                HandleClientDisconnection(client, clientId);
            }
        }

        /// <summary>
        /// 获取客户端ID
        /// </summary>
        private string GetClientId(TcpClient client)
        {
            var ipEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            return ipEndPoint != null ? $"{ipEndPoint.Address}:{ipEndPoint.Port}" : "UnknownClient";
        }

        /// <summary>
        /// 处理客户端断开连接
        /// </summary>
        private void HandleClientDisconnection(TcpClient client, string clientId)
        {
            client.Close();
            _connectedClients.TryTake(out _);
            _clientDictionary.TryRemove(clientId, out _);

            var clientEventArgs = new ClientEventArgs(clientId);
            InvokeSafe(() => OnClientDisconnected?.Invoke(this, clientEventArgs));
            _eventAggregator.GetEvent<ClientDisconnectedEvent>().Publish(clientEventArgs);

            Console.WriteLine($"Client {clientId} disconnected.");
        }

        /// <summary>
        /// 线程安全触发事件
        /// </summary>
        private void InvokeSafe(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during event invocation: {ex.ToString()}");
            }
        }

        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        public async Task<OperateResult> SendAsync(string message)
        {
            var data = Encoding.UTF8.GetBytes(message);
            foreach (var client in _connectedClients)
            {
                if (client.Connected)
                {
                    try
                    {
                        await client.GetStream().WriteAsync(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending to client: {ex.ToString()}");
                    }
                }
            }

            return new OperateResult { IsSuccess = true };
        }

        /// <summary>
        /// 向指定客户端发送消息
        /// </summary>
        public async Task<OperateResult> SendToClientAsync(string clientId, string message)
        {
            if (_clientDictionary.TryGetValue(clientId, out var client) && client.Connected)
            {
                var data = Encoding.UTF8.GetBytes(message);
                try
                {
                    await client.GetStream().WriteAsync(data);
                    Console.WriteLine($"Message sent to client {clientId}");
                    return new OperateResult { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending to client {clientId}: {ex.ToString()}");
                    return new OperateResult { IsSuccess = false, Message = ex.ToString() };
                }
            }

            Console.WriteLine($"Client {clientId} not found or disconnected.");
            return new OperateResult { IsSuccess = false, Message = "Client not found or disconnected." };
        }
    }
}
