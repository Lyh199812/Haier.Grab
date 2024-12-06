using BXC.Client.MinotorModule.Views;
using Base.Client.BLL;
using Base.Client.DAL;
using Base.Client.IBLL;
using Base.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BXC.Client.DAL;
using Base.Client.BXC.IDAL;
using Base.Client.BXC.BLL;
using Base.Client.BXC.IBLL;

namespace BXC.Client.MinotorModule
{
    public class BXCMainInfoModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BXCMonitorView>(); 
            containerRegistry.RegisterForNavigation<BXCHistoryView>(); 
            containerRegistry.Register<IBXCRunLogDAL, BXCRunLogDAL>();
            containerRegistry.RegisterSingleton<IBXCRunLogBLL, BXCRunLogBLL>();
        }
    }
}
