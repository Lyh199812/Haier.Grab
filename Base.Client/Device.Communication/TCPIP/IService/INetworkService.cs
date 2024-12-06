using Base.Client.Entity;
using Device.Events.TCPIP;
using Device.Models.Config.TCPIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Communication.TCPIP
{
    public interface INetworkService
    {
        event EventHandler<DataReceivedEventArgs> OnDataReceived;      // 数据接收事件
        event EventHandler<ClientEventArgs> OnClientConnected;         // 客户端上线事件
        event EventHandler<ClientEventArgs> OnClientDisconnected;      // 客户端下线事件
        event EventHandler<ClientEventArgs> OnClientDisconnectedManually; // 主动断开客户端事件

        Task<OperateResult> StartAsync(string ipAddress, int port);    // 启动服务
        Task<OperateResult> SendAsync(string message);                 // 发送消息给所有客户端
        Task<OperateResult> SendToClientAsync(string clientId, string message); // 发送消息给指定客户端
        Task<OperateResult> DisconnectClientAsync(string clientId);    // 主动断开指定客户端连接
        Task<OperateResult> StopAsync();                               // 停止服务
    }
}
