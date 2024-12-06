using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.SystemModule.Views;

namespace Base.Client.SystemModule
{
    public class SysModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 把默认显示

            // RegionManager
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainHeaderRegion", "MainHeaderView");
            regionManager.RegisterViewWithRegion("LeftMenuTreeRegion", "TreeMenuView");

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainHeaderView>();
            containerRegistry.Register<TreeMenuView>();
        }
    }
}
