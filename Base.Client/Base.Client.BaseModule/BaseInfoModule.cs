using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.BaseModule.Views;

namespace Base.Client.BaseModule
{
    public class BaseInfoModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FileUploadView>();
            containerRegistry.RegisterForNavigation<UserManagementView>();
            containerRegistry.RegisterForNavigation<MenuManagementView>();
            containerRegistry.RegisterForNavigation<RoleManagementView>();
            containerRegistry.RegisterForNavigation<AutoColorView>();
            containerRegistry.RegisterForNavigation<LicenseColorView>();
            containerRegistry.RegisterForNavigation<FeeModeView>();


            containerRegistry.RegisterDialog<AddFileDialog>();
            containerRegistry.RegisterDialog<AddUserDialog>();
            containerRegistry.RegisterDialog<AddMenuDialog>();
            containerRegistry.RegisterDialog<AddRoleDialog>();
            containerRegistry.RegisterDialog<SelectUserDialog>();
            containerRegistry.RegisterDialog<ModifyRolesDialog>();
            containerRegistry.RegisterDialog<AddAutoColorDialog>();
            containerRegistry.RegisterDialog<AddLicenseColorDialog>();
            containerRegistry.RegisterDialog<AddFeeModeDialog>();
        }
    }
}
