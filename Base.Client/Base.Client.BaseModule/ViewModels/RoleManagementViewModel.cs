using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Unity;
using Base.Client.BaseModule.Models;
using Base.Client.Common;
using Base.Client.Entity;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class RoleManagementViewModel : PageViewModelBase
    {
        public ObservableCollection<RoleModel> Roles { get; set; } = new ObservableCollection<RoleModel>();
        public ObservableCollection<UserInfoModel> Users { get; set; } = new ObservableCollection<UserInfoModel>();
        public ObservableCollection<MenuItemModel> Menus { get; set; } = new ObservableCollection<MenuItemModel>();

        private RoleModel _currentRole;
        public RoleModel CurrentRole
        {
            get { return _currentRole; }
            set { this.SetProperty(ref _currentRole, value); }
        }


        IRoleBLL _roleBLL;
        IUserBLL _userBLL;
        IMenuBLL _menuBLL;
        Dispatcher dispatcher;

        [Dependency]
        public IDialogService DialogService { get; set; }


        public ICommand AddUserCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public RoleManagementViewModel(IUnityContainer unityContainer, IRegionManager regionManager, IRoleBLL roleBLL, IUserBLL userBLL, IMenuBLL menuBLL) : base(unityContainer, regionManager)
        {
            _roleBLL = roleBLL;
            _userBLL = userBLL;
            _menuBLL = menuBLL;
            dispatcher = Dispatcher.CurrentDispatcher;

            this.PageTitle = "权限组管理";
            this.AddButtonText = "添加权限组";

            AddUserCommand = new DelegateCommand(DoAddUserCommand);
            SaveCommand = new DelegateCommand(DoSaveCommand);// 用户以及菜单 

            this.Refresh();
        }

        public override void Refresh()
        {
            Roles.Clear();
            CurrentRole = null;
            // 后续做对接
            Task.Run(async () =>
            {
                // 关键字

                var roleList = await _roleBLL.GetAll();

                if (roleList != null)
                {
                    roleList.ForEach(r =>
                    {
                        dispatcher.Invoke(() =>
                        {
                            Roles.Add(new RoleModel
                            {
                                RoleId = r.roleId,
                                RoleName = r.roleName,
                                ItemSelectedCommand = new DelegateCommand<object>(async (obj) =>
                                {
                                    this.CurrentRole = obj as RoleModel;
                                    // 加载当前权限 组的用户和菜单 
                                    await LoadUsersAndMenus();
                                }),
                                DeleteCommand = new DelegateCommand<object>(DoDeleteCommand)
                            });
                        });
                    });

                    if (Roles.Count > 0)
                    {
                        Roles[0].IsSelected = true;
                        CurrentRole = Roles[0];

                        await LoadUsersAndMenus();
                    }
                }
            });
        }

        // 添加权限组
        public override void Add()
        {
            // 打开弹窗肯定到判断返回结果       根据结果可以进行刷新    列表子项添加
            // 进行数据保存    不建议      名称不可以重复
            //

            DialogService.ShowDialog("AddRoleDialog", result =>
            {
                if (result.Result == ButtonResult.OK)
                    this.Refresh();// 
            });
        }
        // 删除权限组
        private void DoDeleteCommand(object obj)
        {
            RoleModel roleModel = (RoleModel)obj;

            // 删除      

        }

        private async Task LoadUsersAndMenus()
        {
            dispatcher.Invoke(() =>
            {
                Users.Clear(); Menus.Clear();
            });
            if (CurrentRole != null)
            {
                // 获取当前角色下所有用户    
                var users = await _userBLL.GetUsersByRole(CurrentRole.RoleId.ToString());
                dispatcher.Invoke(() =>
                {
                    users.ForEach(u => Users.Add(new UserInfoModel
                    {
                        UserId = u.userId,
                        UserName = u.userName,
                        DeleteCommand = new DelegateCommand<object>((obj) =>
                        {
                            Users.Remove(obj as UserInfoModel);
                        })
                    }));
                });
                // 获取当前角色下所有菜单
                var alls = await _menuBLL.GetAllMenus();// 没有根据权限组  获取所有菜单项
                var current = await _menuBLL.GetMenusByRole(CurrentRole.RoleId);// 当前权限组所拥有的菜单项
                // 递归菜单数据

                dispatcher.Invoke(() =>
                {
                    FillMenus(alls, Menus, new MenuItemModel { MenuId = 0 }, current.ToList());
                });
            }
        }

        private void FillMenus(IList<MenuEntity> origMenus, IList<MenuItemModel> menus, MenuItemModel parent, IList<int> role_menus)
        {
            var sub = origMenus.Where(m => m.parentId == parent.MenuId).OrderBy(o => o.index);

            if (sub.Count() > 0)
            {
                foreach (var item in sub)
                {
                    MenuItemModel mm = new MenuItemModel()
                    {
                        MenuId = item.menuId,
                        MenuHeader = item.menuHeader,
                        MenuIcon = item.menuIcon,
                        TargetView = item.targetView,
                        IsExpanded = true,
                        MenuType = item.menuType,
                        ParentId = item.parentId,
                        Index = item.index,
                        IsSelected = role_menus.Contains(item.menuId),
                        Parent = parent
                    };

                    FillMenus(origMenus, mm.Children, mm, role_menus);
                    //mm.HasChild = mm.Children.Count > 0;
                    menus.Add(mm);
                }
            }
        }

        private void DoAddUserCommand()
        {
            // 1、默认新的权限组   内部没有用户，如添加   可以选择所有的用户
            // 2、维护过和权限组   内部有部分用户，添加的时候只选择没有的用户
            DialogParameters param = new DialogParameters();
            param.Add("ids", Users.Select(u => u.UserId).ToList());

            DialogService.ShowDialog(
               "SelectUserDialog",
               param,
               new Action<IDialogResult>(result =>
               {
                   // 每次选择后都进行保存数据   
                   if (result != null && result.Result == ButtonResult.OK)
                   {
                       var users = result.Parameters.GetValue<List<UserInfoModel>>("users");
                       users.ForEach(u => Users.Add(new UserInfoModel
                       {
                           UserId = u.UserId,
                           UserName = u.UserName,
                           RealName = u.RealName,
                           DeleteCommand = new DelegateCommand<object>((obj) =>
                           {
                               Users.Remove(obj as UserInfoModel);
                           })
                       }));
                   }
               })
               );
        }

        List<int> _menusids = new List<int>();
        private void GetSelectedMenuIds(IList<MenuItemModel> menus)
        {
            if (menus != null && menus.Count > 0)
            {
                menus.ToList().ForEach(m =>
                {
                    if (m.IsSelected)
                        _menusids.Add(m.MenuId);

                    if (m.MenuType == 1)
                        GetSelectedMenuIds(m.Children);
                });
            }
        }
        private async void DoSaveCommand()
        {
            // 1、权限组 已经保存到数据库
            // 2、  权限组    用户权限（id）    用户    权限菜单（id）     菜单
            _menusids.Clear();// 菜单的选中结果  所有的选中
            // 
            GetSelectedMenuIds(Menus);
            // 3个参数：权限、用记有、菜单 
            await _roleBLL.SaveRoleInfo(
                new RoleEntity
                {
                    roleId = CurrentRole.RoleId,
                    roleName = CurrentRole.RoleName,
                    state = 1,
                    userIds = Users.Select(u => u.UserId).ToList(),
                    menuIds = _menusids// 树形
                });

            System.Windows.MessageBox.Show("数据保存完成", "提示");
            this.Refresh();
        }
    }
}
