using Base.Client.Common;
using Base.Client.Entity;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Base.Client.SystemModule.Models;
using System.Windows;

namespace Base.Client.SystemModule.ViewModels
{
    public class TreeMenuViewModelIMU
    {
        // 需要多数据库的单列模式的数据转换成树状结构     递归
        public ObservableCollection<MenuItemModel> Menus { get; set; } = new ObservableCollection<MenuItemModel>();

        //public MenuItemModel RootMenu { get; set; } = new MenuItemModel();


        List<MenuEntity> origMenus { get; set; }

        private IRegionManager _regionManager = null;
        public TreeMenuViewModelIMU(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            // 需要初始化的进候进行菜单数据的获取填充
            // 通过Api接口

            Task.Run(async () =>
            {
                if (unityContainer.Resolve<GlobalValue>().IsLocalMode)
                {
                    origMenus = new List<MenuEntity>()
                    {
                        //海尔
                        new MenuEntity()
                        {
                            menuId = 50,
                            menuHeader = "自动抓取",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue60a",
                            index = 0,
                            menuType = 0,  // 顶级菜单
                            state = 1
                        },
                         new MenuEntity()
                        {
                            menuId = 500,
                            menuHeader = "自动抓取",
                            targetView = "GrabLocatorMonitorView",
                            parentId = 50,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,  
                            state = 1
                        },

                        //综合项目
                        new MenuEntity()
                        {
                            menuId = 69,
                            menuHeader = "DataHub",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue60a",
                            index = 0,
                            menuType = 0,  // 顶级菜单
                            state = 1
                        },
                         
                        // 上下料   
                        new MenuEntity
                        {
                            menuId = 71,
                            menuHeader = "在线监控",
                            targetView = "MonitorView",
                            parentId = 69,
                            menuIcon = "",
                            index = 0,
                            menuType = 1,  // 顶级菜单
                            state = 1
                        },
                        new MenuEntity
                        {
                            menuId = 72,
                            menuHeader = "托盘状态",
                            targetView = "TrayView",
                            parentId = 69,
                            menuIcon = "",
                            index = 0,
                            menuType = 1,  // 顶级菜单
                            state = 1
                        },
                        new MenuEntity
                        {
                            menuId = 73,
                            menuHeader = "批次查询",
                            targetView = "BatchView",
                            parentId = 69,
                            menuIcon = "",
                            index = 0,
                            menuType = 1,  // 顶级菜单
                            state = 1
                        },
                         new MenuEntity()
                        {
                            menuId = 74,
                            menuHeader = "测试数据",
                            targetView = "TestDataView",
                            parentId = 69,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,  // 顶级菜单
                            state = 1
                        },
                         //综合项目
                        new MenuEntity()
                        {
                            menuId = 67,
                            menuHeader = "机器视觉",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue60a",
                            index = 0,
                            menuType = 0,  // 顶级菜单
                            state = 1
                        },
                         new MenuEntity()
                        {
                            menuId = 67,
                            menuHeader = "运动控制",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue60a",
                            index = 0,
                            menuType = 0,  // 顶级菜单
                            state = 1
                        },
                          new MenuEntity()
                        {
                            menuId = 67,
                            menuHeader = "仪器通信",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue60a",
                            index = 0,
                            menuType = 0,  // 顶级菜单
                            state = 1
                        },
                          
                          
                    };
                    unityContainer.Resolve<Dispatcher>().Invoke(() =>
                    {
                        this.FillMenus(Menus, 0);
                        switch(1)
                        {
                            case 0:
                                {
                                    //灌胶
                                    OpenDefaultView(Menus, "PGlueLocatorMonitorView");
                                    break;
                                }
                               
                            case 1:
                                {
                                    OpenDefaultView(Menus, "MonitorView");
                                    break;
                              
                                }
                            case 2:
                                {
                                    OpenDefaultView(Menus, "BXCMonitorView");
                                    break;

                                }
                        }
                       
                    });
                }
                else
                {
                    origMenus = unityContainer.Resolve<GlobalValue>().UserInfo.menus;
                    // origMenus = await menuBLL.GetMenus(0);// 通过接口获取 菜单 数据       

                    unityContainer.Resolve<Dispatcher>().Invoke(() =>
                    {
                        this.FillMenus(Menus, 0);
                    });
                }
               
            });

            //origMenus = menuBLL.GetMenus(0).GetAwaiter().GetResult();
            //this.FillMenus(Menus, 0);
        }

        ///递归
        ///
        private void FillMenus(ObservableCollection<MenuItemModel> menus, int parentId)
        {
            // 根据菜单ID查找对应的直接子节点，第一层的菜单，父节点为0
            var sub = origMenus.Where(m => m.parentId == parentId).OrderBy(o => o.index);

            if (sub.Count() > 0)
            {
                foreach (var item in sub)
                {
                    //MenuItemModel mm = new MenuItemModel(_regionManager)
                    MenuItemModel mm = new MenuItemModel(_regionManager)
                    {
                        MenuHeader = item.menuHeader,
                        MenuIcon = item.menuIcon,
                        TargetView = item.targetView
                    };
                    menus.Add(mm);

                    FillMenus(mm.Children, item.menuId);
                }
            }
        }


        // 打开默认的窗口
        private void OpenDefaultView(ObservableCollection<MenuItemModel> menus, string targetView)
        {
            // 假设要打开第一个菜单项的默认页面
            //var defaultMenuItem = Menus.FirstOrDefault();
            Application.Current.Dispatcher.Invoke
                (() => {
                    var defaultMenuItem = FindMenuItem(menus, targetView);
                    if (defaultMenuItem != null )
                    {
                        defaultMenuItem.OpenViewCommand.Execute(defaultMenuItem);
                    };
                });

        }
        public MenuItemModel FindMenuItem(ObservableCollection<MenuItemModel> menus, string targetView)
        {
            foreach (var menuItem in menus)
            {
                // 检查当前项是否匹配
                if (menuItem.TargetView == targetView)
                    return menuItem;

                // 如果当前项有子项，递归搜索子项
                var childResult = FindMenuItem(menuItem.Children, targetView);
                if (childResult != null)
                    return childResult;
            }
            return null; // 如果没有找到，返回 null
        }
    }
}
