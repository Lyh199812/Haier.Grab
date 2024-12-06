using Prism.Ioc;
using Prism.Modularity;
using Project.IMU.DataHub.BLL;
using Project.IMU.DataHub.DAL;
using Project.IMU.DataHub.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub
{
    public class IMUDataHubModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MonitorView>();
            containerRegistry.RegisterForNavigation<TrayView>();
            containerRegistry.RegisterForNavigation<BatchView>();
            containerRegistry.RegisterForNavigation<TestDataView>();

            containerRegistry.Register<IMUDataHubDBConfig>();
            containerRegistry.Register<TestConfigDAL>();
            containerRegistry.Register<TestConfigService>();
            containerRegistry.Register <DeviceInfoDAL>();
            containerRegistry.Register<DeviceInfoService>();
            containerRegistry.Register<TrayDAL>();
            containerRegistry.Register<TrayService>();
            containerRegistry.Register<BatchDAL>();
            containerRegistry.Register<BatchService>();
            containerRegistry.Register<BatchTestDataDAL>();
            containerRegistry.Register<BatchTestDataService>();

        }
    }
}
