using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Base.Client.Upgrade.ViewModels;
using Base.Client.Upgrade.Views;

namespace Base.Client.Upgrade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string files)
        {
            InitializeComponent();

            //this.txt.Text = files;

            // Blend
            // 1、先用分号分割
            // 2、再用|分割

            this.DataContext = new MainWindowViewModel(files)
            {
                ConfirmAction = OnFinished
            };
        }

        private void OnFinished()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                // 打开升级完成确认窗口
                new FinishWindow() { Owner = this }.ShowDialog();
            });
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
