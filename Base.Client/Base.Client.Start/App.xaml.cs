using Base.Client.BLL;
using Base.Client.BLL.Controls;
using Base.Client.Common;
using Base.Client.DAL;
using Base.Client.DAL.Controls.ProductionInfoCard;
using Base.Client.EFCore;
using Base.Client.IBLL;
using Base.Client.IBLL.Controls.ProductionInfoCard;
using Base.Client.IDAL;
using Base.Client.IDAL.Controls;
using Base.Client.Views;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace Base.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
       public App()
        {
           
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // 获取 ModuleCatalog
            var moduleCatalog = (IModuleCatalog)Container.Resolve<IModuleCatalog>();

            foreach (var moduleInfo in moduleCatalog.Modules)
            {
                // 动态实例化模块类型
                var moduleType = Type.GetType(moduleInfo.ModuleType);
                if (moduleType != null)
                {
                    var module = Container.Resolve(moduleType) as IModule;
                    if (module is IDisposable disposableModule)
                    {
                        disposableModule.Dispose();
                    }
             
                }
            }
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<Views.MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            if (true)//本地模式
            {
                return;
            }
            if (Container.Resolve<Views.WinLogin>().ShowDialog() == false)
                //    shell.Show();// 不能使用ShowDialog，会导致后续Module无法加载
                //else
                Environment.Exit(0);// 直接强制退出
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterSingleton(typeof(GlobalValue), () => new GlobalValue());
            containerRegistry.Register(typeof(MessageService), () => new MessageService(Container.Resolve<IDialogService>()));
            //ConfigurationManager    包一层
            containerRegistry.Register(typeof(Dispatcher), obj => Application.Current.Dispatcher);
            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.Register<IWebDataAccess, WebDataAccess>();
            containerRegistry.Register<ILocalDataAccess, LocalDataAccess>();

            // Dal的注册
            containerRegistry.Register<BaseDBConfig>();

            containerRegistry.Register<ILoginDal, LoginDal>();
            containerRegistry.Register<IFileDal, FileDal>();
            containerRegistry.Register<IMenuDal, MenuDal>();
            containerRegistry.Register<IUserDal, UserDal>();
            containerRegistry.Register<IRoleDal, RoleDal>();
            containerRegistry.Register<IAutoDal, AutoDal>();
            containerRegistry.Register<IBaseDal, BaseDal>();
            containerRegistry.Register<IRecordDal, RecordDal>();
            containerRegistry.Register<IPayDal, PayDal>();
            containerRegistry.Register<IReportDal, ReportDal>();
            containerRegistry.Register<IRunLogDAL, RunLogDAL>();
            containerRegistry.Register<IProductionInfoCardDAL, ProductionInfoCardDAL>();
            // 注册 DbContext
            var optionsBuilder = new DbContextOptionsBuilder<BaseDBConfig>();
            containerRegistry.Register<BaseDBConfig>();

            // BLL的注册
            containerRegistry.Register<ILoginBLL, LoginBLL>();
            containerRegistry.Register<IFileBLL, FileBLL>();
            containerRegistry.Register<IMenuBLL, MenuBLL>();
            containerRegistry.Register<IUserBLL, UserBLL>();
            containerRegistry.Register<IRoleBLL, RoleBLL>();
            containerRegistry.Register<IAutoBLL, AutoBLL>();
            containerRegistry.Register<IBaseBLL, BaseBLL>();
            containerRegistry.Register<IRecordBLL, RecordBLL>();
            containerRegistry.Register<IPayBLL, PayBLL>();
            containerRegistry.Register<IMonitorBLL, MonitorBLL>();
            containerRegistry.Register<IReportBLL, ReportBLL>();
            containerRegistry.Register <IRunLogBLL, RunLogBLL>();
            containerRegistry.Register <IProductionInfoCardBLL, ProductionInfoCardBLL>();


            // 注册弹出窗口的父窗口
            containerRegistry.RegisterDialogWindow<Views.DialogWindow>();
            containerRegistry.RegisterDialog<MessageView>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            // MessageBox.Show("Wait");
            // 自动更新的时候    自动扫描目录
            return new DirectoryModuleCatalog()
            {
                ModulePath = Environment.CurrentDirectory + "\\Modules"
            };
        }
    }
}
