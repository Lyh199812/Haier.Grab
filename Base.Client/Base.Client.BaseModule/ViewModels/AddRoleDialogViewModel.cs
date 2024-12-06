using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.Entity;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class AddRoleDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "添加权限组信息";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters) { }





        private string _roleName;
        public string RoleName
        {
            get => _roleName;
            set { SetProperty<string>(ref _roleName, value); }
        }

        public ICommand ConfirmCommand
        {
            get => new DelegateCommand(async () =>
            {
                // 保存权限组信息到数据库
                var result = await _roleBLL.AddRole(new RoleEntity { roleId = 0, roleName = this.RoleName, state = 1 });

                // 保存成功   再做关闭
                if (result)
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                else
                {
                    // 异常提示
                }
            });
        }
        public ICommand CancelCommand
        {
            get => new DelegateCommand(() =>
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
            });
        }

        DialogCloseListener IDialogAware.RequestClose => throw new NotImplementedException();

        IRoleBLL _roleBLL;
        public AddRoleDialogViewModel(IRoleBLL roleBLL)
        {
            _roleBLL = roleBLL;
        }
    }
}
