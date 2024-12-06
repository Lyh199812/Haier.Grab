using Prism.Events;
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
using System.Windows.Shapes;
using Base.Client.Common;

namespace Base.Client.Views
{
    /// <summary>
    /// WinLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WinLogin : Window
    {
        public WinLogin(IEventAggregator ea)
        {
            InitializeComponent();

            ea.GetEvent<CloseWindowEvent>().Subscribe(MessageReceived, ThreadOption.UIThread);
        }
        private void MessageReceived()
        {
            this.Close();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    // 判断用户信息   调用接口    界面业务逻辑   VM
        //    this.DialogResult = true;// 关闭窗口操作
        //}
    }
}
