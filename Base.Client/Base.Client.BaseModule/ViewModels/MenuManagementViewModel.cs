using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Unity;
using Base.Client.BaseModule.Models;
using Base.Client.Common;
using Base.Client.Entity;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class MenuManagementViewModel : PageViewModelBase
    {
        public ObservableCollection<MenuItemModel> Menus { get; set; } = new ObservableCollection<MenuItemModel>();
        private List<MenuEntity> origMenus = null;
        private MenuItemModel currentMenu;

        public ICommand ItemSelectedCommand { get; set; }// 绑定给每个子项


        IUnityContainer _unityContainer;
        IMenuBLL _menuBLL;
        IDialogService _dialogService;
        MessageService _messageService;
        public MenuManagementViewModel(IUnityContainer unityContainer, IRegionManager regionManager, IMenuBLL menuBLL, IDialogService dialogService, MessageService messageService)
        : base(unityContainer, regionManager)
        {
            _unityContainer = unityContainer;
            _menuBLL = menuBLL;
            _dialogService = dialogService;
            _messageService = messageService;

            this.PageTitle = "系统菜单管理";

            //ItemSelectedCommand = new DelegateCommand<object>(o => { currentMenu = o as MenuItemModel; });

            this.Refresh();
        }

        public override void Refresh()
        {
            // 调用BLL
            // 打开加载动画（目前主窗口没有加载动画，可以参照登录窗口来处理）
            Menus.Clear();
            Task.Run(async () =>
            {
                origMenus = await _menuBLL.GetAllMenus();   // 单列的数据   树状

                MenuItemModel root = new MenuItemModel()
                {
                    MenuId = 0,
                    MenuHeader = "根节点",
                    IsExpanded = true,
                    IsCurrent = true,
                    MenuType = 1// 有子节点
                };
                root.DropCommand = new DelegateCommand<object>(obj => OnDropCommand(obj, root));
                currentMenu = root;// 作用是用来处理添加菜单时使用
                root.Selected = new Action(() => { currentMenu = root; });

                // 开始递归
                // 需要在UI线程
                _unityContainer.Resolve<Dispatcher>().Invoke(() =>
                {
                    Menus.Add(root); ;
                    FillMenus(root, root.MenuId);
                });
            });
        }

        private void FillMenus(MenuItemModel parent, int parentId)
        {
            // 根据菜单ID查找对应的直接子节点，第一层的菜单，父节点为0
            var sub = origMenus.Where(m => m.parentId == parentId).OrderBy(o => o.index);

            if (sub.Count() > 0)
            {
                foreach (var item in sub)
                {
                    //MenuItemModel mm = new MenuItemModel(_regionManager)
                    MenuItemModel mm = new MenuItemModel()
                    {
                        MenuId = item.menuId,
                        MenuHeader = item.menuHeader,
                        MenuIcon = item.menuIcon,
                        TargetView = item.targetView,
                        IsExpanded = true,
                        MenuType = item.menuType,
                        //ParentId = item.parentId,
                        Index = item.index,
                        Parent = parent
                    };
                    // 命令
                    mm.EditCommand = new DelegateCommand<object>(EditMenuNode);
                    mm.DeleteCommand = new DelegateCommand<object>(DeleteMenuNode);
                    mm.Selected = new Action(() => { currentMenu = mm; });

                    mm.MouseMoveCommand = new DelegateCommand<object>(obj => this.OnMouseMoveCommand(obj, mm));
                    mm.DropCommand = new DelegateCommand<object>(obj => OnDropCommand(obj, mm));
                    mm.DragOverCommand = new DelegateCommand<object>(obj => OnDragOverCommand(obj, mm));
                    mm.DragLeaveCommand = new DelegateCommand<object>(obj => OnDragLeaveCommand(obj, mm));


                    parent.Children.Add(mm);

                    FillMenus(mm, item.menuId);
                }
                parent.Children.Last().IsLastChild = true;
            }
        }

        private void EditMenuNode(object obj)
        {
            DialogParameters param = new DialogParameters();
            param.Add("mode", 1);
            param.Add("menu", obj);

            ShowEditDialog(param);
        }
        private void DeleteMenuNode(object obj)
        {

        }
        private void OnMouseMoveCommand(object obj, MenuItemModel model)
        {
            // 鼠标左键按下的时候才表示拖动开始
            MouseEventArgs ea = obj as MouseEventArgs;
            if (ea == null) return;

            if (ea.LeftButton != MouseButtonState.Pressed) return;

            //创建一个拖动对象，传一个节点过去
            DragDrop.DoDragDrop(ea.OriginalSource as FrameworkElement, model, DragDropEffects.Move);
        }

        private void OnDropCommand(object obj, MenuItemModel targetModel)
        {
            DragEventArgs ea = obj as DragEventArgs;
            ea.Handled = true;
            // 被拖动的Model
            var model = ea.Data.GetData(typeof(MenuItemModel)) as MenuItemModel;

            // 当前拖动的对象放到目标对象的前面
            if (targetModel.OverLocation == 1)
            {
                model.Parent.Children.Remove(model);
                targetModel.Parent.Children.Insert(targetModel.Index, model);
                model.Parent = targetModel.Parent;
            }
            else if (targetModel.OverLocation == 3)// 当前拖动对象放置到目标对象的后面
            {
                model.Parent.Children.Remove(model);
                int index = targetModel.Index + 1;
                if (index + 1 < targetModel.Parent.Children.Count)
                    targetModel.Parent.Children.Insert(targetModel.Index + 1, model);
                else
                    targetModel.Parent.Children.Add(model);

                model.Parent = targetModel.Parent;
            }
            else if (targetModel.OverLocation == 2)// 当前拖动对象放置到目标对象的子项
            {
                model.Parent.Children.Remove(model);
                targetModel.Children.Add(model);
                model.Parent = targetModel;
            }
            targetModel.OverLocation = 0;

            // 
            int count = targetModel.Parent.Children.Count;
            for (int i = 0; i < count; i++)
            {
                targetModel.Parent.Children[i].Index = i;
                targetModel.Parent.Children[i].IsLastChild = false;
            }
            if (count > 0)
                targetModel.Parent.Children.Last().IsLastChild = true;

            count = targetModel.Children.Count;
            for (int i = 0; i < count; i++)
            {
                targetModel.Children[i].Index = i;
                targetModel.Children[i].IsLastChild = false;
            }
            if (count > 0)
                targetModel.Children.Last().IsLastChild = true;
        }

        private void OnDragOverCommand(object obj, MenuItemModel targetModel)
        {
            DragEventArgs ea = obj as DragEventArgs;// 实际的页面对象   不是Model
            ea.Handled = true;

            targetModel.OverLocation = 0;// 光标移动的位置
            var model = ea.Data.GetData(typeof(MenuItemModel)) as MenuItemModel;
            if (model == null || model.MenuId == targetModel.MenuId) return;

            TreeViewItem tvi = this.FindParent<TreeViewItem>(ea.Source as DependencyObject);
            ////RadioButton rb = this.FindParent<RadioButton>(ea.Source as DependencyObject);

            Point point = ea.GetPosition(tvi);
            if (point.Y <= 5) targetModel.OverLocation = 1;
            else if (targetModel.MenuType == 1 && point.Y > 5 && point.Y < tvi.ActualHeight - 5) targetModel.OverLocation = 2;
            else if ((!targetModel.IsExpanded || targetModel.MenuType == 0) && targetModel.Children.Count == 0 && point.Y >= tvi.ActualHeight - 5) targetModel.OverLocation = 3;
            else targetModel.OverLocation = 0;

            //System.Diagnostics.Debug.WriteLine(tvi.ActualHeight + "-----" + OverLocation);
        }
        private void OnDragLeaveCommand(object obj, MenuItemModel targetModel)
        {
            targetModel.OverLocation = 0;
        }

        private T FindParent<T>(DependencyObject i_dp) where T : DependencyObject
        {
            DependencyObject dobj = VisualTreeHelper.GetParent(i_dp);
            if (dobj != null)
            {
                if (dobj is T)
                {
                    return (T)dobj;
                }
                else
                {
                    dobj = FindParent<T>(dobj);
                    if (dobj != null && dobj is T)
                    {
                        return (T)dobj;
                    }
                }
            }
            return null;
        }

        public override void Add()
        {
            if (currentMenu.MenuType == 1)// 只有是
            {
                DialogParameters param = new DialogParameters();
                param.Add("mode", 0);
                param.Add("menu", currentMenu);// 当前菜单项

                ShowEditDialog(param);
            }
        }

        private void ShowEditDialog(DialogParameters param)
        {
            param.Add("parentNodes", origMenus);// 筛选   集合类型的节点

            _dialogService.ShowDialog(
                "AddMenuDialog",
                param,
                new Action<IDialogResult>(result =>
                {
                    if (result != null && result.Result == ButtonResult.OK)
                    {
                        var state = _messageService.Show("数据已保存", "提示");
                        //System.Windows.MessageBox.Show("数据已保存", "提示");
                        this.Refresh();
                    }
                }));
        }
    }
}
