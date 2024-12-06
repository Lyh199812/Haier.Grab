using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Base.Client.BaseModule.Models;
using Base.Client.Entity;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class AddMenuDialogViewModel : BindableBase, IDialogAware
    {
        // 这里需要动态处理  （新增/编辑）
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var mode = parameters.GetValue<int>("mode");

            origMenus = parameters.GetValue<List<MenuEntity>>("parentNodes");

            var pList = origMenus.Where(m => m.menuType == 1).ToList();
            pList.Insert(0, new MenuEntity { menuId = 0, menuHeader = "根节点" });
            pList.ForEach(m => ParentNodes.Add(
                new MenuItemModel
                {
                    MenuId = m.menuId,
                    MenuHeader = m.menuHeader,
                }
                ));

            var mi = parameters.GetValue<MenuItemModel>("menu");

            if (mode == 0)
            {
                // 新增的时候   表示当前节点信息
                Title = "添加新菜单";
                CurrentParentMenu = ParentNodes.First(m => m.MenuId == mi.MenuId);
            }
            else if (mode == 1)// 表示编辑 
            {
                // 编辑的时候，传当前对象
                Title = "编辑菜单信息";
                CurrentParentMenu = ParentNodes.First(m => m.MenuId == mi.ParentId);
                MenuInfo = mi;
            }
            MenuInfo.ParentId = mi.ParentId;
        }

        public MenuItemModel MenuInfo { get; set; } = new MenuItemModel();
        private List<MenuEntity> origMenus = null;

        public ObservableCollection<MenuItemModel> ParentNodes { get; set; } = new ObservableCollection<MenuItemModel>();

        private MenuItemModel _currentParentMenu;
        public MenuItemModel CurrentParentMenu
        {
            get => _currentParentMenu;
            set { SetProperty<MenuItemModel>(ref _currentParentMenu, value); }
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        DialogCloseListener IDialogAware.RequestClose => throw new NotImplementedException();

        public AddMenuDialogViewModel(IMenuBLL menuBLL)
        {
            ConfirmCommand = new DelegateCommand<object>(async (obj) =>
            {
                try
                {
                    // 保存
                    var result = await menuBLL.SaveMenu(new MenuEntity
                    {
                        menuId = MenuInfo.MenuId,
                        menuHeader = MenuInfo.MenuHeader,
                        targetView = MenuInfo.TargetView,
                        parentId = CurrentParentMenu.MenuId,
                        menuIcon = MenuInfo.MenuIcon,// 字体图标
                        index = MenuInfo.Index,
                        menuType = MenuInfo.MenuType,
                        state = 1
                    });

                    if (result.state == 200)
                    {
                        // 关闭弹窗
                        DialogResult dialogResult = new DialogResult(ButtonResult.OK);
                        RequestClose?.Invoke(dialogResult);
                    }
                    else
                    {
                        //result.exceptionMessage
                    }
                }
                catch (Exception ex)
                {
                    //提示一下异常信息
                }
            });



            CancelCommand = new DelegateCommand(() =>
            {
                // 关闭弹窗
                RequestClose?.Invoke(new DialogResult());
            });
        }
    }
}
