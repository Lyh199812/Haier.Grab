using Prism.Ioc;
using Prism.Modularity;
using Project.Modules.GlueLocator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Modules.GlueLocator
{
    public class GlueLocatorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PGlueLocatorMonitorView>();
        }
    }
}
