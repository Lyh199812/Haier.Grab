using Base.Client.Common;
using Base.Client.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Project.Modules.GlueLocator.ViewModels
{
    public class MirrorViewModel : PageViewModelBase
    {
        public MirrorViewModel(IUnityContainer unityContainer, IRegionManager regionManager,  IRoleBLL roleBLL, IUserBLL userBLL, IMenuBLL menuBLL)
            : base(unityContainer, regionManager)
        {
            this.PageTitle = "灌胶监控";

        }
    }
}
