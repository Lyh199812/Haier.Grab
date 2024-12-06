using Base.Clent.HVisionModule.Preprocessing.Views;
using Base.Clent.HVisionModule.TemplateMatch.Views;
using HVisionLibs.Core.Preprocessing;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.HVisionModule
{
    public class HVisionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //镜像
            containerRegistry.Register<MirrorService>();
            containerRegistry.RegisterForNavigation<MirrorView>();
            //缩放因子
            containerRegistry.Register<ZoomFactorService>();
            containerRegistry.RegisterForNavigation<ZoomFactorView>();


            //NCC模板创建
            containerRegistry.RegisterForNavigation<NCCTemplateCreatorView>();
            containerRegistry.RegisterForNavigation<NCCTemplateSearcherView>();

            //Shape模板创建
            containerRegistry.RegisterForNavigation<ShapeTemplateCreatorView>();
            containerRegistry.RegisterForNavigation<ShapeTemplateSearcherView>();
        }
    }
}
