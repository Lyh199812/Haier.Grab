using Base.Client.Common;
using Base.Client.Entity;
using Device.DataConvertLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Device.Communication
{

    public class MyTCPClient
    {
        #region 字段与属性
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        /// <summary>
        /// 发送超时时间
        /// </summary>
        public int SendTimeOut { get; set; } = 2000;

        /// <summary>
        /// 接收超时时间
        /// </summary>
        public int ReceiveTimeOut { get; set; } = 1000;

        //创建一个Socket对象
        private Socket socket { get; set; }

        /// <summary>
        ///  锁对象
        /// </summary>
        private SimpleHybirdLock hybirdLock = new SimpleHybirdLock();

        /// <summary>
        /// 每次接收前延时的时间
        /// </summary>
        public int SleepTime { get; set; } = 30;

        /// <summary>
        /// 最大的等待次数
        /// </summary>
        public int MaxWaitTimes { get; set; } = 20;

        /// <summary>
        /// 单元标识符
        /// </summary>
        public byte SlaveId { get; set; } = 0x01;

        #endregion

        #region 建立连接与断开连接
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <returns>返回结果</returns>
        public OperateResult Connect(string ip, int port)
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = SendTimeOut,
                ReceiveTimeout = ReceiveTimeOut
            };

            try
            {
                if (IPAddress.TryParse(ip, out IPAddress ipAddress))
                {
                    this.socket.Connect(ipAddress, port);
                }
                else
                {
                    this.socket.Connect(ip, port);
                }
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult($"Connection failed: {ex.ToString()}");
            }
        }

        /// <summary>
        /// 建立连接并开启监听
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <returns>返回结果</returns>
        public OperateResult ConnectWithListen(string ip, int port)
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = SendTimeOut,
                ReceiveTimeout = ReceiveTimeOut
            };

            try
            {
                if (IPAddress.TryParse(ip, out IPAddress ipAddress))
                {
                    this.socket.Connect(ipAddress, port);
                }
                else
                {
                    this.socket.Connect(ip, port);
                }

                // 开启监听
                Task.Run(() => ReceiveMessages(this.socket));
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult($"Connection and listen failed: {ex.ToString()}");
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns>返回结果</returns>
        public OperateResult Disconnect()
        {
            try
            {
                this.socket?.Close();
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult($"Disconnection failed: {ex.ToString()}");
            }
        }
        #endregion

        #region 通用发送并接收方法
        /// <summary>
        /// 发送并接收方法
        /// </summary>
        /// <param name="send">发送报文</param>
        /// <param name="receive">接收报文</param>
        /// <returns>返回结果</returns>
        public OperateResult SendAndReceive(byte[] send, out byte[] receive)
        {
            hybirdLock.Enter();
            receive = null;
            byte[] buffer = new byte[1024];
            MemoryStream stream = new MemoryStream();

            try
            {
                socket.Send(send, send.Length, SocketFlags.None);
                int timer = 0;

                while (true)
                {
                    Thread.Sleep(SleepTime);

                    if (socket.Available > 0)
                    {
                        int count = socket.Receive(buffer, SocketFlags.None);
                        stream.Write(buffer, 0, count);
                    }
                    else if (stream.Length > 0 || timer > MaxWaitTimes)
                    {
                        break;
                    }
                    else
                    {
                        timer++;
                    }
                }

                if (stream.Length > 0)
                {
                    receive = stream.ToArray();
                    return OperateResult.CreateSuccessResult();
                }
                return OperateResult.CreateFailResult("Receive timed out.");
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult($"Send and receive failed: {ex.ToString()}");
            }
            finally
            {
                hybirdLock.Leave();
            }
        }
        #endregion

        #region 异步接收
        private async Task ReceiveMessages(Socket socket)
        {
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                    if (bytesRead == 0)
                    {
                        OnMessageReceived(new MessageReceivedEventArgs("Server closed the connection.", true));
                        break;
                    }

                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    OnMessageReceived(new MessageReceivedEventArgs(response, false));
                }
            }
            catch (SocketException se)
            {
                OnMessageReceived(new MessageReceivedEventArgs($"Socket error while receiving data: {se.Message}", true));
            }
            catch (Exception e)
            {
                OnMessageReceived(new MessageReceivedEventArgs($"Error receiving data: {e.Message}", true));
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Console.WriteLine("Connection closed.");
            }
        }

        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        #endregion




    }
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; }
        public bool IsClosed { get; }

        public MessageReceivedEventArgs(string message, bool isClosed)
        {
            Message = message;
            IsClosed = isClosed;
        }
    }
}
