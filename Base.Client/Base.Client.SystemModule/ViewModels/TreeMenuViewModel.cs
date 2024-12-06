using Base.Client.Common;
using Base.Client.Entity;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Base.Client.SystemModule.Models;
using System.Windows;

namespace Base.Client.SystemModule.ViewModels
{
    public class TreeMenuViewModel
    {
        // 需要多数据库的单列模式的数据转换成树状结构     递归
        public ObservableCollection<MenuItemModel> Menus { get; set; } = new ObservableCollection<MenuItemModel>();

        //public MenuItemModel RootMenu { get; set; } = new MenuItemModel();


        List<MenuEntity> origMenus { get; set; }

        private IRegionManager _regionManager = null;
        public TreeMenuViewModel(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            // 需要初始化的进候进行菜单数据的获取填充
            // 通过Api接口

            Task.Run(async () =>
            {
                if (unityContainer.Resolve<GlobalValue>().IsLocalMode) 
                {   
                    origMenus = new List<MenuEntity>
                                        {
                                            //综合项目
                                            new MenuEntity()
                                            {
                                                 menuId = 69,
                                                menuHeader = "综合项目",
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
                                                menuHeader = "产品抓取",
                                                targetView = "",
                                                parentId = 69,
                                                menuIcon = "\ue601",
                                                index = 0,
                                                menuType = 0,  // 顶级菜单
                                                state = 1
                                            },
                                            new MenuEntity
                                            {
                                                menuId = 710,
                                                menuHeader = "抓取监控",
                                                targetView = "GrabLocatorMonitorView",
                                                parentId = 71,
                                                menuIcon = "",
                                                index = 0,
                                                menuType = 0,  // 顶级菜单
                                                state = 1
                                            },
                            
                                            new MenuEntity
                                            {
                                                menuId = 711,
                                                menuHeader = "抓取配方",
                                                targetView = "ProductionConfigView",
                                                parentId = 71,
                                                menuIcon = "",
                                                index = 0,
                                                menuType = 0,  // 顶级菜单
                                                state = 1
                                            },
                                            //灌胶定位
                                             new MenuEntity
                                            {
                                                menuId = 70,
                                                menuHeader = "灌胶定位",
                                                targetView = "",
                                                parentId = 69,
                                                menuIcon = "\ue924",
                                                index = 0,
                                                menuType = 0,  // 顶级菜单
                                                state = 1
                                            },
                                             new MenuEntity
                                            {
                                                menuId = 700,
                                                menuHeader = "灌胶运行监控",
                                                targetView = "PGlueLocatorMonitorView",
                                                parentId = 70,
                                                menuIcon = "",
                                                index = 0,
                                                menuType = 1,  // 顶级菜单
                                                state = 1
                                            },

                                            // 机器视觉
                                            new MenuEntity
                                            {
                                                menuId = 20,
                                                menuHeader = "机器视觉",
                                                targetView = "",
                                                parentId = 0,
                                                menuIcon = "\ue646",
                                                index = 0,
                                                menuType = 0,  // 顶级菜单
                                                state = 1
                                            },
                                            //机器视觉-助手
                                           new MenuEntity
                                            {
                                                menuId = 220,
                                                menuHeader = "助手",
                                                targetView = "",
                                                parentId = 20,
                                                menuIcon = "\ue63f",  // 可以选择适合的图标
                                                index = 0,
                                                menuType = 0,  // 父菜单
                                                state = 1
                                            },
                                             new MenuEntity
                                            {
                                                menuId = 2202,
                                                menuHeader = "图像采集",
                                                targetView = "",
                                                parentId = 200,
                                                menuIcon = "",  // 可以选择适合的图标
                                                index = 0,
                                                menuType = 0,  // 父菜单
                                                state = 1
                                            },
                                            new MenuEntity
                                            {
                                                menuId = 2201,
                                                menuHeader = "相机标定",
                                                targetView = "",
                                                parentId = 200,
                                                menuIcon = "\ue63f",  // 可以选择适合的图标
                                                index = 0,
                                                menuType = 0,  // 父菜单
                                                state = 1
                                            },

                                            // 预处理
                                            new MenuEntity
                        {
                            menuId = 200,
                            menuHeader = "预处理",
                            targetView = "",
                            parentId = 20,
                            menuIcon = "\ue78f",
                            index = 0,
                            menuType = 0,  // 子菜单
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2000,
                            menuHeader = "镜像反转",
                            targetView = "MirrorView",
                            parentId = 200,
                            menuIcon = "",
                            index = 0,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2001,
                            menuHeader = "缩放因子设置",
                            targetView = "ZoomFactorView",
                            parentId = 200,
                            menuIcon = "",
                            index = 1,
                            menuType = 1,  // 具体操作
                            state = 1
                        },

                                            // 模板匹配
                                            new MenuEntity
                        {
                            menuId = 201,
                            menuHeader = "模板匹配",
                            targetView = "",
                            parentId = 20,
                            menuIcon = "\ue600",
                            index = 0,
                            menuType = 0,  // 子菜单
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2100,
                            menuHeader = "形状模板创建",
                            targetView = "ShapeTemplateCreatorView",
                            parentId = 201,
                            menuIcon = "",
                            index = 0,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2101,
                            menuHeader = "形状模板搜索",
                            targetView = "ShapeTemplateSearcherView",
                            parentId = 201,
                            menuIcon = "",
                            index = 1,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2102,
                            menuHeader = "NCC模板创建",
                            targetView = "NCCTemplateCreatorView",
                            parentId = 201,
                            menuIcon = "",
                            index = 2,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2103,
                            menuHeader = "NCC模板搜索",
                            targetView = "NCCTemplateSearcherView",
                            parentId = 201,
                            menuIcon = "",
                            index = 3,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2104,
                            menuHeader = "形变模板创建",
                            targetView = "DeformationTemplateCreatorView",
                            parentId = 201,
                            menuIcon = "",
                            index = 4,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2105,
                            menuHeader = "形变模板搜索",
                            targetView = "DeformationTemplateSearcherView",
                            parentId = 201,
                            menuIcon = "",
                            index = 5,
                            menuType = 1,  // 具体操作
                            state = 1
                        },

                                            // 比较测量
                                            new MenuEntity
                        {
                            menuId = 203,
                            menuHeader = "比较测量",
                            targetView = "",
                            parentId = 20,
                            menuIcon = "\ue62b",
                            index = 0,
                            menuType = 0,  // 子菜单
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2300,
                            menuHeader = "卡尺找圆",
                            targetView = "CaliperCircleSearchView",
                            parentId = 203,
                            menuIcon = "",
                            index = 0,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2301,
                            menuHeader = "颜色检测",
                            targetView = "ColorDetectionView",
                            parentId = 203,
                            menuIcon = "",
                            index = 1,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 2302,
                            menuHeader = "几何测量",
                            targetView = "GeometricMeasurementView",
                            parentId = 203,
                            menuIcon = "",
                            index = 2,
                            menuType = 1,  // 具体操作
                            state = 1
                        },

                                            // 视觉识别
                                            new MenuEntity
                        {
                            menuId = 204,
                            menuHeader = "视觉识别",
                            targetView = "",
                            parentId = 20,
                            menuIcon = "\ue63d",
                            index = 0,
                            menuType = 0,  // 子菜单
                            state = 1
                        },
                                            new MenuEntity
                    {
                        menuId = 206,
                        menuHeader = "目标检测",
                        targetView = "ObjectDetectionView",
                        parentId = 204,
                        menuIcon = "\ue64d",  // 例如一个目标检测的图标
                        index = 0,
                        menuType = 0,
                        state = 1
                    },
                                            new MenuEntity
                    {
                        menuId = 207,
                        menuHeader = "人脸识别",
                        targetView = "FaceRecognitionView",
                        parentId = 204,
                        menuIcon = "\ue64e",  // 例如一个人脸识别的图标
                        index = 1,
                        menuType = 0,
                        state = 1
                    },              
                                            new MenuEntity
                    {
                        menuId = 208,
                        menuHeader = "车牌识别",
                        targetView = "PlateRecognitionView",
                        parentId = 204,
                        menuIcon = "\ue64f",  // 例如一个车牌识别的图标
                        index = 2,
                        menuType = 0,
                        state = 1
                    },              
                                            new MenuEntity
                    {
                        menuId = 209,
                        menuHeader = "二维码识别",
                        targetView = "QRCodeRecognitionView",
                        parentId = 204,
                        menuIcon = "\ue650",  // 例如一个二维码识别的图标
                        index = 3,
                        menuType = 0,
                        state = 1
                    },                   
                                            new MenuEntity
                    {
                        menuId = 210,
                        menuHeader = "条形码识别",
                        targetView = "BarcodeRecognitionView",
                        parentId = 204,
                        menuIcon = "\ue651",  // 例如一个条形码识别的图标
                        index = 4,
                        menuType = 0,
                        state = 1
                    },                    
                                            new MenuEntity
                    {
                        menuId = 211,
                        menuHeader = "物体分类",
                        targetView = "ObjectClassificationView",
                        parentId = 204,
                        menuIcon = "\ue652",  // 例如一个物体分类的图标
                        index = 5,
                        menuType = 0,
                        state = 1
                    },     
                                            new MenuEntity
                    {
                        menuId = 212,
                        menuHeader = "图像分割",
                        targetView = "ImageSegmentationView",
                        parentId = 204,
                        menuIcon = "\ue653",  // 例如一个图像分割的图标
                        index = 6,
                        menuType = 0,
                        state = 1
                    },
                                            new MenuEntity
                    {
                        menuId = 213,
                        menuHeader = "OCR识别",
                        targetView = "OCRRecognitionView",
                        parentId = 204,
                        menuIcon = "\ue654",  // 例如一个OCR识别的图标
                        index = 7,
                        menuType = 0,
                        state = 1
                    },


                                            // 缺陷检测
                                            new MenuEntity
                        {
                            menuId = 205,
                            menuHeader = "缺陷检测",
                            targetView = "",
                            parentId = 20,
                            menuIcon = "\ue68b",
                            index = 0,
                            menuType = 0,  // 子菜单
                            state = 1
                        },
                                            new MenuEntity
                    {
                        menuId = 2050,
                        menuHeader = "形状检测",
                        targetView = "ShapeDefectDetectionView",
                        parentId = 205,
                        menuIcon = "",
                        index = 0,
                        menuType = 0,  // 子菜单
                        state = 1
                        // 形状检测功能，基于图像的几何特征（如边缘、角度等）识别图像中的缺陷，例如裂纹、凹陷等形状异常。
                    },
                                            new MenuEntity
                    {
                        menuId = 2051,
                        menuHeader = "颜色检测",
                        targetView = "ColorDefectDetectionView",
                        parentId = 205,
                        menuIcon = "",
                        index = 1,
                        menuType = 0,  // 子菜单
                        state = 1
                        // 颜色检测功能，通过检测物体的颜色变化来识别缺陷，如颜色不均、变色、污染等。
                    },
                                            new MenuEntity
                    {
                        menuId = 2052,
                        menuHeader = "缺陷分类",
                        targetView = "DefectClassificationView",
                        parentId = 205,
                        menuIcon = "",
                        index = 2,
                        menuType = 0,  // 子菜单
                        state = 1
                        // 缺陷分类功能，通过机器学习或深度学习对检测到的缺陷进行分类，判断缺陷类型，如划痕、裂纹、凹陷等。
                    },  
                                            new MenuEntity
                    {
                        menuId = 2053,
                        menuHeader = "缺陷定位",
                        targetView = "DefectLocalizationView",
                        parentId = 205,
                        menuIcon = "",
                        index = 3,
                        menuType = 0,  // 子菜单
                        state = 1
                        // 缺陷定位功能，通过图像处理技术（如边缘检测、模板匹配）准确定位缺陷的位置，用于后续的修复或分析。
                    },

                                            // 运动控制
                                            new MenuEntity
                        {
                            menuId = 30,
                            menuHeader = "运动控制",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue670",
                            index = 0,
                            menuType = 0,  // 顶级菜单
                            state = 1
                        },
                                            new MenuEntity
                    {
                        menuId = 301,
                        menuHeader = "位置控制",
                        targetView = "PositionControlView",
                        parentId = 30,
                        menuIcon = "\ue62d",  // 例如一个位置的图标
                        index = 0,
                        menuType = 0,
                        state = 1
                    },
                                            new MenuEntity
                    {
                        menuId = 302,
                        menuHeader = "速度控制",
                        targetView = "SpeedControlView",
                        parentId = 30,
                        menuIcon = "\ue62e",  // 例如一个速度的图标
                        index = 1,
                        menuType = 0,
                        state = 1
                    },
                                            new MenuEntity
                    {
                        menuId = 303,
                        menuHeader = "加速度控制",
                        targetView = "AccelerationControlView",
                        parentId = 30,
                        menuIcon = "\ue62f",  // 例如一个加速度的图标
                        index = 2,
                        menuType = 0,
                        state = 1
                    },
                                            new MenuEntity
                    {
                        menuId = 304,
                        menuHeader = "路径规划",
                        targetView = "PathPlanningView",
                        parentId = 30,
                        menuIcon = "\ue630",  // 例如一个路径规划的图标
                        index = 3,
                        menuType = 0,
                        state = 1
                    },
                                            new MenuEntity
                    {
                        menuId = 305,
                        menuHeader = "运动轨迹仿真",
                        targetView = "MotionTrajectorySimulationView",
                        parentId = 30,
                        menuIcon = "\ue631",  // 例如一个轨迹仿真的图标
                        index = 4,
                        menuType = 0,
                        state = 1
                    },
                                            // 通信协议
                                            new MenuEntity
                        {
                            menuId = 40,
                            menuHeader = "通信协议",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue63f",
                            index = 0,
                            menuType = 0,  // 顶级菜单
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 400,
                            menuHeader = "TCPClient",
                            targetView = "TCPClientView",
                            parentId = 40,
                            menuIcon = "",
                            index = 0,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 401,
                            menuHeader = "TCPServer",
                            targetView = "TCPServerView",
                            parentId = 40,
                            menuIcon = "",
                            index = 1,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 402,
                            menuHeader = "SerialPort",
                            targetView = "SerialPortView",
                            parentId = 40,
                            menuIcon = "",
                            index = 2,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 403,
                            menuHeader = "CANBus",
                            targetView = "CANBusView",
                            parentId = 40,
                            menuIcon = "",
                            index = 3,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 404,
                            menuHeader = "ModbusTcpClient",
                            targetView = "ModbusTcpClientView",
                            parentId = 40,
                            menuIcon = "",
                            index = 4,
                            menuType = 1,  // 具体操作
                            state = 1
                        },
                                            new MenuEntity
                        {
                            menuId = 405,
                            menuHeader = "ModbusTcpServer",
                            targetView = "ModbusTcpServerView",
                            parentId = 40,
                            menuIcon = "",
                            index = 5,
                            menuType = 1,  // 具体操作
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
                                    OpenDefaultView(Menus, "GrabLocatorMonitorView");
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
