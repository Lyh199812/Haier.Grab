using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Base.Client.SystemModule.Models
{
    public class MenuItemModel : BindableBase
    {
        public string MenuIcon { get; set; }
        public string MenuHeader { get; set; }
        public string TargetView { get; set; }// 双击这个节点的时候打开的页面

        private bool _isExpanded;
        // 是否展开节点
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }
        // 子节点
        public ObservableCollection<MenuItemModel> Children { get; set; } = new ObservableCollection<MenuItemModel>();


        IRegionManager _regionManager = null;
        // 不是由IoC容器创建的，所以不能做注入
        public MenuItemModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private ICommand _openViewCommand;

        public ICommand OpenViewCommand
        {
            get
            {
                if (_openViewCommand == null)
                    _openViewCommand = new DelegateCommand<MenuItemModel>(DoOpenViewCommand);
                return _openViewCommand;
            }
        }
        private void DoOpenViewCommand(MenuItemModel model)
        {
            // 获取到需要打开的View名称，   RegionManager
            // 没有子项的节点并且设置了打开页面的节点   可以进行操作，
            if ((model.Children == null || model.Children.Count == 0) &&
                          !string.IsNullOrEmpty(model.TargetView))
                _regionManager.RequestNavigate("MainViewRegion", model.TargetView);
            else
                IsExpanded = !IsExpanded;

        }
    }
}
