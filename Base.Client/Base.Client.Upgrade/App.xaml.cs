using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Base.Client.Upgrade
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">启动程序时传入的值</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            //MessageBox.Show(e.Args.Length.ToString());
            if (e.Args == null || e.Args.Length == 0)
                Application.Current.Shutdown();

            new MainWindow(e.Args[0]).Show();
        }
    }
}
