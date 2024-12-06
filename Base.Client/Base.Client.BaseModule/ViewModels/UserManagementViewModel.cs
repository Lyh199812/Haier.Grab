using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Unity;
using Base.Client.BaseModule.Models;
using Base.Client.Common;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class UserManagementViewModel : PageViewModelBase
    {
        public ObservableCollection<UserInfoModel> Users { get; set; } = new ObservableCollection<UserInfoModel>();

        IUserBLL _userBLL;
        IUnityContainer _unityContainer;
        IDialogService _dialogService;
        public UserManagementViewModel(IUnityContainer unityContainer, IRegionManager regionManager, IUserBLL userBLL, IDialogService dialogService) : base(unityContainer, regionManager)
        {
            this.PageTitle = "用户信息管理";

            _unityContainer = unityContainer;
            _userBLL = userBLL;
            _dialogService = dialogService;

            Refresh();
        }

        // RerfreshCommand=new DelegateCommand
        public override void Refresh()
        {
            Users.Clear();
            // 刷新用户信息的逻辑
            Task.Run(async () =>
            {
                //  从WebApi获取所有用户信息
                // 还有异常需要 处理
                // 有特殊字符的传输风险   /
                var users = await _userBLL.GetAll(this.Keyword);

                if (users != null)
                {
                    // 需要在UI线程处理
                    _unityContainer.Resolve<Dispatcher>().Invoke(() =>
                    {
                        users.ForEach(u => Users.Add(
                            new UserInfoModel
                            {
                                Index = users.IndexOf(u) + 1,
                                UserId = u.userId,
                                UserName = u.userName,
                                UserIcon = "http://localhost:22643/api/" + u.userIcon,
                                Age = u.age,
                                Password = u.password,
                                RealName = u.realName,
                                Roles = new ObservableCollection<RoleModel>(u.roles.Select(r => new RoleModel { RoleId = r.roleId, RoleName = r.roleName })),

                                EditCommand = new DelegateCommand<UserInfoModel>(EditUserInfo),
                                DeleteCommand = new DelegateCommand<object>(DeleteItem),
                                RoleCommand = new DelegateCommand<object>(SetRoles),
                                PwdCommand = new DelegateCommand<object>(SetPassword)
                            }
                            ));
                    });
                }
            });
        }

        public override void Add()
        {
            // 打开新增/编辑子窗口
            DialogParameters param = new DialogParameters();
            param.Add("mode", 0);
            _dialogService.ShowDialog("AddUserDialog", param, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                    this.Refresh();
            });
        }

        private void EditUserInfo(UserInfoModel model)
        {
            DialogParameters param = new DialogParameters();
            param.Add("mode", 1);
            param.Add("data", model);
            _dialogService.ShowDialog("AddUserDialog", param, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                    this.Refresh();
            });
        }

        //private void ShowModify(DialogParameters param, Action<IDialogResult> callback) => _dialogService.ShowDialog("AddUserDialog", param, callback);

        private async void DeleteItem(object obj)
        {
            // 所有的删除动作，都需要提示，防止误操作
            if (System.Windows.MessageBox.Show("是否确定删除此用户信息？", "提示", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
            {
                // 
                //var model = obj as UserInfoModel;
                //if (model != null)
                //    await _userBLL.ChangeState(model.UserId, 0);// 逻辑删除

                this.Refresh();
            }
        }

        private void SetRoles(object obj)
        {
            DialogParameters param = new DialogParameters();
            param.Add("userId", (obj as UserInfoModel).UserId);// Dialog进行数据保存，知道对哪个用户进行操作
            param.Add("roles", (obj as UserInfoModel).Roles.Select(r => r.RoleId).ToList());

            _dialogService.ShowDialog(
                "ModifyRolesDialog",
                param,
                new Action<IDialogResult>(result =>
                {
                    if (result != null && result.Result == ButtonResult.OK)
                    {
                        System.Windows.MessageBox.Show("角色分配完成", "提示");
                        this.Refresh();
                    }
                }));
        }
        private void SetPassword(object obj)
        {
            Task.Run(async () =>
            {
                try
                {
                    bool result = await _userBLL.ResetPassword((obj as UserInfoModel).UserId);
                    if (result)
                        System.Windows.MessageBox.Show("密码已重置", "提示");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString(), "提示");
                }
            });
        }

    }
}
