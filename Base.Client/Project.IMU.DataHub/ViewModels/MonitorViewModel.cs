using Base.Client.Common;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IBLL.Controls.ProductionInfoCard;
using Base.Client.Tools;
using Device.CommLab.ViewModels;
using Device.CommLab.Views;
using Device.Communication.TCPIP;
using Device.Events.TCPIP;
using Device.Models.Config;
using Device.Models.Enum;
using Project.IMU.DataHub.BLL;
using Project.IMU.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Documents;

namespace Project.IMU.DataHub.ViewModels
{
    public class MonitorViewModel : PageViewModelBase
    {
        IEventAggregator _eventAggregator;
        TestConfigService _testCfg;
        DeviceInfoService _device;
        BatchService _batchs;
        public MonitorViewModel(BatchService batchs, TestConfigService testCfg, DeviceInfoService device, IUnityContainer unityContainer, IRegionManager regionManager, IEventAggregator eventAggregator, IProductionInfoCardBLL _productionInfoCardBLL, IRunLogBLL _runLogBLL, IRoleBLL roleBLL, IUserBLL userBLL, IMenuBLL menuBLL)
            : base(unityContainer, regionManager)
        {
            this.PageTitle = "在线监控";
            this.IsCanClose = false;
            LogGridStatus = "UnLock";
            productionInfoCardBLL = _productionInfoCardBLL;
            runLogBLL = _runLogBLL;

            GetTCPServer(eventAggregator);
            _eventAggregator = eventAggregator;
            // 订阅 DeviceDataReceivedEvent 事件
            _eventAggregator.GetEvent<DeviceDataReceivedEvent>().Subscribe(OnDeviceDataReceived);
            _eventAggregator.GetEvent<ClientConnectedEvent>().Subscribe(OnClientConnected);
            _eventAggregator.GetEvent<ClientDisconnectedEvent>().Subscribe(OnClientDisconnected);
            //
            _testCfg = testCfg;
            var rstLoadTestConfig = _testCfg.Load(1);
            if (rstLoadTestConfig.IsSuccess)
            {
                TestConfig = rstLoadTestConfig.Content;
                runLogBLL.AddSuccessLog("加载TestConfig成功");
            }
            else
            {
                runLogBLL.AddErrorLog("软件启动条件不满足-加载TestConfig失败");
                return;

            }
            //
            _device = device;
            var rstResetdevice = _device.ResetAllConnectionStates();
            if (rstResetdevice.IsSuccess)
            {
                runLogBLL.AddSuccessLog("复位设备连接状态成功");

            }
            else
            {
                runLogBLL.AddErrorLog("软件启动条件不满足-复位设备连接状态失败"+ rstResetdevice.Message);
                return;

            }
            var rstLoaddevice = _device.LoadDevices();
            if (rstLoaddevice.IsSuccess)
            {
                DeviceInfos =new ObservableCollection<TDeviceInfo>(rstLoaddevice.Content);
                runLogBLL.AddSuccessLog("加载设备表成功");
            }
            else
            {
                runLogBLL.AddErrorLog("软件启动条件不满足-加载设备表失败" + rstLoaddevice.Message);
                return;
            }
            //
            var rst = productionInfoCardBLL.LoadLatestRecord();
            if (rst.IsSuccess)
            {
                ClearCommand = new DelegateCommand(ClearProductInfo);
                runLogBLL.AddSuccessLog("加载生产表成功");

            }
            else
            {
                runLogBLL.AddErrorLog("软件启动条件不满足-加载设备表失败" + rst.Message);
                return;

            }

            //
            _batchs = batchs;
            var rst2=_batchs.LoadByStatus("进行中");
            if (rst2.IsSuccess) 
            {
                InProgressBatches = new ObservableCollection<TBatch>(rst2.Content);
                runLogBLL.AddSuccessLog("加载进行中批次信息成功");

            }
            else
            {
                runLogBLL.AddErrorLog("软件启动条件不满足-加载进行中批次信息失败" + rst.Message);
                return;
            }


        }



        #region Base
        #region property
        public IRunLogBLL runLogBLL { get; set; }


        //ProductionInfo
        private IProductionInfoCardBLL _productionInfoCardBLL;
        public IProductionInfoCardBLL productionInfoCardBLL
        {
            get { return _productionInfoCardBLL; }
            set { _productionInfoCardBLL = value; RaisePropertyChanged(); }
        }
        public DelegateCommand ClearCommand { get; set; }


        #endregion
        public void ClearProductInfo()
        {
            productionInfoCardBLL.RecalculateAndCreateNewRecord();
        }

        #endregion


        #region TCPServer
        private object tcpServer;
        public object TcpServer
        {
            get { return tcpServer; }
            set { tcpServer = value; RaisePropertyChanged(); }
        }

        // 已连接的客户端列表
        private ObservableCollection<string> _connectedClients;
        public ObservableCollection<string> ConnectedClients
        {
            get => _connectedClients ??= new ObservableCollection<string>() { "All" };
            set => SetProperty(ref _connectedClients, value);
        }

        private void GetTCPServer(IEventAggregator eventAggregator)
        {
            TcpServer = new NetworkAssistsView(eventAggregator, TCPIPProtocolsEnum.TCPServer);
        }
        string ReceivedData;
        private void OnDeviceDataReceived(DataReceivedEventArgs data)
        {
            // 处理接收到的数据
            ReceivedData = data.Message;
            runLogBLL.AddInfoLog(data.SenderIpAddress+ data.Message);
        }

        private void OnClientDisconnected(ClientEventArgs args)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ConnectedClients.Remove(args.ClientId);
                TDeviceInfo curDevice = DeviceInfos.Where(x => x.ClientId.Contains(args.ClientId.Split(":")[0])).FirstOrDefault();
                if (curDevice != null)
                {
                    var rst = _device.DeviceOffline(curDevice);
                    if (rst.IsSuccess)
                    {
                        DeviceInfos.Remove(curDevice);
                        DeviceInfos.Insert(0,curDevice);
                        runLogBLL.AddWarningLog($"设备【{curDevice.DeviceName}】下线");
                    }
                    else
                    {
                        runLogBLL.AddWarningLog($"设备【{curDevice.DeviceName}】上线,状态更新失败");

                    }
                    RaisePropertyChanged("DeviceInfos");

                }
                else
                {
                    runLogBLL.AddWarningLog($"未配置客户端【{args.ClientId}】下线");

                }
            }));
        }
        
        private void OnClientConnected(ClientEventArgs args)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ConnectedClients.Add(args.ClientId);
                TDeviceInfo curDevice = DeviceInfos.Where(x => x.ClientId.Contains(args.ClientId.Split(":")[0])).FirstOrDefault();
                if (curDevice != null)
                {
                   
                    var rst= _device.DeviceOnline(curDevice);
                    DeviceInfos.Remove(curDevice);
                    DeviceInfos.Add(curDevice);
                    if (rst.IsSuccess)
                    {
                        runLogBLL.AddSuccessLog($"设备【{curDevice.DeviceName}】上线成功");
                    }
                    else
                    {
                        runLogBLL.AddWarningLog($"设备【{curDevice.DeviceName}】上线,状态更新失败");

                    }
                }
                else
                {
                    runLogBLL.AddWarningLog($"未配置客户端【{args.ClientId}】上线,警告");

                }
            }));
        }
        #endregion

        #region TestConfig
        //TestConfig 
        private string _LogGridStatus;
        public string LogGridStatus
        {
            get { return _LogGridStatus; }
            set
            {
                _LogGridStatus = value;
                RaisePropertyChanged();
            }
        }

        private bool _LogGridIsReadOnly=true;
        public bool LogGridIsReadOnly
        {
            get { return _LogGridIsReadOnly; }
            set { _LogGridIsReadOnly = value; RaisePropertyChanged(); }
        }

        private TTestConfig _TestConfig;
        public TTestConfig TestConfig
        {
            get { return _TestConfig; }
            set { _TestConfig = value; RaisePropertyChanged(); }
        }
        public DelegateCommand OnSetTestConfigStatusCommand => new DelegateCommand(OnSetTestConfigStatus);
        public void OnSetTestConfigStatus()
        {
            if(LogGridIsReadOnly)
            {
                //如果有解锁动作
                LogGridStatus = "Lock";
            }
            else
            {
                LogGridStatus = "UnLock";

            }
            LogGridIsReadOnly = !LogGridIsReadOnly;
        }

        public DelegateCommand OnSaveTestConfigStatusCommand => new DelegateCommand(OnSaveTestConfigStatus);
        public void OnSaveTestConfigStatus()
        {
            var rst=_testCfg.Save(TestConfig);
            if(rst.IsSuccess)
            {
                MessageBox.Show("保存成功");

            }
            else
            {
                MessageBox.Show("保存失败\r\n"+ rst.Message);
            }
        }


        #endregion
           
        #region TDeviceInfo
            
        private ObservableCollection<TDeviceInfo> _DeviceInfos;
        public ObservableCollection<TDeviceInfo> DeviceInfos
        {
            get { return _DeviceInfos; }
            set { _DeviceInfos = value;RaisePropertyChanged(); }
        }

        #endregion

        #region BatchInfo
        private ObservableCollection<TBatch> _InProgressBatches;
        public ObservableCollection<TBatch> InProgressBatches
        {
            get { return _InProgressBatches; }
            set { _InProgressBatches = value; }
        }

        #endregion
    }
}
