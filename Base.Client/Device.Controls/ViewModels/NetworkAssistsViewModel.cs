using Device.Communication.TCPIP;
using Device.Events.TCPIP;
using Device.Models.Enum;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Device.CommLab.ViewModels
{
    public class NetworkAssistsViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private INetworkService _networkService;

        public NetworkAssistsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // 初始化协议列表
            Protocols = new ObservableCollection<string>(Enum.GetNames(typeof(TCPIPProtocolsEnum)));

            // 初始化默认值
            SelectedProtocol = Protocols[0]; // 默认选择第一个协议
            IsProtocolSelectionEnabled = true;
        }

        #region Properties

        // 是否启动服务
        private bool _isServerRunning;
        public bool IsServerRunning
        {
            get => _isServerRunning;
            set => SetProperty(ref _isServerRunning, value);
        }

        // 当前选中的协议
        private string _selectedProtocol;
        public string SelectedProtocol
        {
            get => _selectedProtocol;
            set
            {
                if (SetProperty(ref _selectedProtocol, value))
                {
                    SwitchNetworkService(value); // 切换协议时，重新初始化服务
                }
            }
        }

        // 本地 IP 地址
        private string _localIPAddress="127.0.0.1";
        public string LocalIPAddress
        {
            get => _localIPAddress;
            set => SetProperty(ref _localIPAddress, value);
        }

        // 本地端口
        private string _localPort="1122";
        public string LocalPort
        {
            get => _localPort;
            set => SetProperty(ref _localPort, value);
        }

        // 是否允许选择协议
        private bool _isProtocolSelectionEnabled;
        public bool IsProtocolSelectionEnabled
        {
            get => _isProtocolSelectionEnabled;
            set => SetProperty(ref _isProtocolSelectionEnabled, value);
        }

        // 接收到的数据
        private string _sendAndReceivedData;
        public string SendAndReceivedData
        {
            get => _sendAndReceivedData;
            set
            {
                if (value != null && Encoding.UTF8.GetByteCount(value) > 2 * 1024 * 1024) // 检查大小是否超过 5MB
                {
                    _sendAndReceivedData = string.Empty; // 超过 2MB 时清空
                }
                else
                {
                    SetProperty(ref _sendAndReceivedData, value); // 正常设置值
                }
            }
        }

        // 要发送的文本
        private string _sendText;
        public string SendText
        {
            get => _sendText;
            set => SetProperty(ref _sendText, value);
        }

        // 已连接的客户端列表
        private ObservableCollection<string> _connectedClients;
        public ObservableCollection<string> ConnectedClients
        {
            get => _connectedClients ??= new ObservableCollection<string>() { "All"};
            set => SetProperty(ref _connectedClients, value);
        }

        // 选中的客户端
        private string _selectedClient;
        public string SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        // 所有协议类型
        public ObservableCollection<string> Protocols { get; }

        #endregion

        #region Commands

        // 启动/停止服务
        public ICommand StartStopCommand => new DelegateCommand(async () => await ExecuteStartStopCommandAsync());

        // 发送数据
        public ICommand SendCommand => new DelegateCommand(async () => await SendAsync());

        // 断开客户端
        public ICommand DisconnectClientCommand => new DelegateCommand(DisconnectClient);

        // 清除显示区
        public ICommand ClearDisplayCommand=>new DelegateCommand(ClearDisplay);
        #endregion

        #region Methods

        // 切换网络服务
        private void SwitchNetworkService(string protocolType)
        {
            //_networkService?.Dispose(); // 释放现有服务

            // 根据协议类型创建新的服务
            _networkService = protocolType switch
            {
                nameof(TCPIPProtocolsEnum.TCPServer) => new TcpServerService(_eventAggregator),
                //nameof(TCPIPProtocolsEnum.TCPClient) => new TcpClientService(_eventAggregator),
                //nameof(TCPIPProtocolsEnum.UDP) => new UdpService(_eventAggregator),
                _ => throw new NotSupportedException("Unsupported protocol type")
            };

            // 绑定数据接收事件
            _networkService.OnDataReceived += NetworkService_OnDataReceived;
            _networkService.OnClientConnected += NetworkService_OnClientConnected;
            _networkService.OnClientDisconnected += NetworkService_OnClientDisconnected;
        }

        // 启动或停止服务
        private async Task ExecuteStartStopCommandAsync()
        {
            if (IsServerRunning)
            {
                await StopAsync();
                IsServerRunning = false;
            }
            else
            {
                await StartAsync();
                IsServerRunning = true;
            }
        }

        // 启动服务
        private async Task StartAsync()
        {
            if (int.TryParse(LocalPort, out int port))
            {
                await _networkService.StartAsync(LocalIPAddress, port);
            }
        }

        // 停止服务
        private async Task StopAsync()
        {
            await _networkService.StopAsync();
        }

        // 发送数据
        private async Task SendAsync()
        {
            if (!string.IsNullOrEmpty(SendText))
            {
                if (string.IsNullOrEmpty(SelectedClient) || SelectedProtocol.Contains("All"))
                {
                    await _networkService.SendAsync(SendText);

                }
                else
                {
                    await _networkService.SendToClientAsync(SelectedClient, SendText);
                }
                SendAndReceivedData += $"[Sent {DateTime.Now.ToString("HH:mm:ss:ff")}]\r\n {SendText}\n"; // 更新显示
            }
        }

        // 断开客户端连接
        private void DisconnectClient()
        {
            if (!string.IsNullOrEmpty(SelectedClient))
            {
                _networkService.DisconnectClientAsync(SelectedClient);
                ConnectedClients.Remove(SelectedClient);
                SelectedClient = null;
            }
        }

        // 数据接收事件处理
        private void NetworkService_OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            SendAndReceivedData += $"[Received from {e.SenderIpAddress} {DateTime.Now.ToString("HH:mm:ss:ff")}]\r\n {e.Message}\n";
        }

        // 客户端连接事件处理
        private void NetworkService_OnClientConnected(object sender, ClientEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ConnectedClients.Add(e.ClientId);

            }));
        }

        // 客户端断开事件处理
        private void NetworkService_OnClientDisconnected(object sender, ClientEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ConnectedClients.Remove(e.ClientId);
            }));
        }


        //清除显示区
        private void ClearDisplay()
        {
            SendAndReceivedData = "";
        }
        #endregion
    }
}
