using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Device.Events.TCPIP;

namespace Device.Communication.TCPIP
{
    public class TcpClientService //: INetworkService
    {
        private readonly IEventAggregator _eventAggregator;
        private TcpClient _client;
        private NetworkStream _stream;
        private CancellationTokenSource _cts;

        public event EventHandler<DataReceivedEventArgs> OnDataReceived;
        public TcpClientService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        // 启动TCP客户端并连接到指定的服务器
        public async Task StartAsync(string ipAddress, int port)
        {
            _cts = new CancellationTokenSource();
            _client = new TcpClient();

            try
            {
                // 尝试连接到服务器
                await _client.ConnectAsync(IPAddress.Parse(ipAddress), port);
                _stream = _client.GetStream();
                Console.WriteLine($"Connected to TCP Server {ipAddress}:{port}");

                // 启动接收数据的任务
                _ = Task.Run(async () =>
                {
                    var buffer = new byte[1024];
                    while (!_cts.Token.IsCancellationRequested && _client.Connected)
                    {
                        try
                        {
                            int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, _cts.Token);
                            if (bytesRead > 0)
                            {
                                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                Console.WriteLine($"Received: {receivedMessage}");

                                // 获取发送方的 IP 地址
                                string senderIpAddress = _client.Client.RemoteEndPoint.ToString();

                                // 触发数据接收事件，传递消息和发送方的 IP 地址
                                OnDataReceived?.Invoke(this, new DataReceivedEventArgs(receivedMessage, senderIpAddress));

                            }
                        }
                        catch (OperationCanceledException)
                        {
                            // 任务取消
                            break;
                        }
                        catch (Exception ex)
                        {
                            // 捕获其他异常
                            Console.WriteLine($"Error receiving data: {ex.ToString()}");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                // 捕获连接错误
                Console.WriteLine($"Connection error: {ex.ToString()}");
                throw;
            }
        }

        // 发送数据到服务器
        public async Task SendAsync(string message)
        {
            if (_stream != null && _client.Connected)
            {
                try
                {
                    var data = Encoding.UTF8.GetBytes(message);
                    await _stream.WriteAsync(data, 0, data.Length);
                    Console.WriteLine($"Sent: {message}");
                }
                catch (Exception ex)
                {
                    // 捕获发送错误
                    Console.WriteLine($"Error sending data: {ex.ToString()}");
                }
            }
            else
            {
                Console.WriteLine("TCP Client is not connected.");
            }
        }

        // 停止客户端服务并关闭连接
        public Task StopAsync()
        {
            _cts?.Cancel();

            // 关闭网络流和客户端连接
            _stream?.Close();
            _client?.Close();
            Console.WriteLine("TCP Client stopped.");

            return Task.CompletedTask;
        }

        // 向特定客户端发送数据的功能不被支持，抛出异常
        public async Task SendToSpecificClientAsync(TcpClient targetClient, string message)
        {
            // 如果此方法被调用，抛出异常，表明此功能不支持
            throw new InvalidOperationException("TCP Client cannot send data to a specific client directly.");
        }

        public Task SendToClientAsync(string clientId, string message)
        {
            throw new NotImplementedException();
        }
    }




}
