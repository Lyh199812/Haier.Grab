using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.Common.Attibutes;

namespace Base.Client.BaseModule.Models
{
    public class MenuItemModel : BindableBase
    {
        // 从数据库中获取的都是大于0的id
        public int MenuId { get; set; }
        // 排序
        public int Index { get; set; }
        private int _parentId;
        public int ParentId
        {
            get => _parentId;
            set { SetProperty<int>(ref _parentId, value); }
        }

        private int _menuType;
        public int MenuType
        {
            get => _menuType;
            set { SetProperty<int>(ref _menuType, value); }
        }
        //public bool HasChildre { get; set; }


        private string _menuIcon;
        public string MenuIcon
        {
            get { return _menuIcon; }
            set { SetProperty(ref _menuIcon, value); }
        }

        private string _menuHeader;
        /// <summary>
        /// 菜单的标题
        /// </summary>
        [Uniqueness("菜单名称已存在")]
        public string MenuHeader
        {
            get { return _menuHeader; }
            set { SetProperty(ref _menuHeader, value); }
        }

        private string _targetView;
        public string TargetView
        {
            get => _targetView;
            set { SetProperty<string>(ref _targetView, value); }
        }

        //private ObservableCollection<MenuItemModel> _children = new ObservableCollection<MenuItemModel>();
        //public ObservableCollection<MenuItemModel> Children
        //{
        //    get => _children;
        //    set { SetProperty<ObservableCollection<MenuItemModel>>(ref _children, value); }
        //}
        private MenuItemModel _parent;
        public MenuItemModel Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                this.ParentId = value.MenuId;
            }
        }

        public ObservableCollection<MenuItemModel> Children { get; set; } = new ObservableCollection<MenuItemModel>();

        private bool _isLastChild = false;
        /// <summary>
        /// 是否是最后一个子节点
        /// </summary>
        public bool IsLastChild
        {
            get { return _isLastChild; }
            set { SetProperty(ref _isLastChild, value); }
        }

        private bool _isExpanded;
        // 节点是否展开
        public bool IsExpanded
        {
            get => _isExpanded;
            set { SetProperty<bool>(ref _isExpanded, value); }
        }


        private bool _isSelected;
        // 是否选中    -》权限管理-》针对某个权限选择一些菜单
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty<bool>(ref _isSelected, value, () =>
                {
                    if (!value && Children != null)
                        Children.ToList().ForEach(m => m.IsSelected = false);
                    if (value && Parent != null && Parent.MenuId > 0)
                        Parent.IsSelected = true;
                    if (!value && Parent != null && !Parent.Children.ToList().Exists(c => c.IsSelected))
                    {
                        Parent.IsSelected = false;
                    }
                });
            }
        }

        private bool _isCurrent;
        public bool IsCurrent
        {
            get => _isCurrent;
            set
            {
                SetProperty<bool>(ref _isCurrent, value);
                if (value)
                    Selected?.Invoke();
            }
        }

        private int _overLocation = 0;
        public int OverLocation
        {
            get { return _overLocation; }
            set
            {
                SetProperty<int>(ref _overLocation, value);
            }
        }



        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public Action Selected { get; set; }



        public ICommand MouseMoveCommand { get; set; }
        public ICommand DropCommand { get; set; }
        public ICommand DragOverCommand { get; set; }

        public ICommand DragLeaveCommand { get; set; }
    }
}
