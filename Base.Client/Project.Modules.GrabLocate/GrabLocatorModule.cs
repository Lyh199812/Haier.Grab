using Base.Client.BLL;
using Base.Client.DAL;
using Base.Client.IBLL;
using Base.Client.IDAL;
using MvCamCtrl.NET;
using Prism.Ioc;
using Prism.Modularity;
using Project.Modules.GlueLocator.Views;
using Project.Modules.GrabLocate;
using Project.Modules.GrabLocate.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Modules.GlueLocator
{
    public class GrabLocatorModule : IModule,IDisposable
    {
        private readonly  IContainerProvider _containerProvider;


        public GrabLocatorModule(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

        }
        public void Dispose()
        {
            // 解析 CameraController 并调用其 Dispose 方法
            var cameraController = _containerProvider.Resolve<CameraController>();
            cameraController.Dispose();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<GrabLocatorMonitorView>();
            containerRegistry.RegisterForNavigation<ProductionConfigView>();

            
            containerRegistry.RegisterForNavigation<IRunLogBLL, RunLogBLL>();
            containerRegistry.RegisterForNavigation<IRunLogDAL, RunLogDAL>();
            containerRegistry.RegisterSingleton<CameraController>();
            containerRegistry.Register<CommonConfigDAL>(); 
            containerRegistry.Register<CommonConfigService>();
            containerRegistry.Register<ProductConfigDAL>();

            

        }
    }
}
