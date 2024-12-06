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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Base.Client.Upgrade.Controls
{
    /// <summary>
    /// WaterProgress.xaml 的交互逻辑
    /// </summary>
    public partial class WaterProgress : UserControl
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(WaterProgress), new PropertyMetadata(0.0,new PropertyChangedCallback(OnValueChanged)));

        private static void OnValueChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            // 获取当前Value的最新值
            double newValue = (double)e.NewValue;  // 0-100 以  160/100=1.6

            // 配置一个简单线性动画
            DoubleAnimation da = new DoubleAnimation(160 - newValue * 1.6, TimeSpan.FromMilliseconds(200));
            (d as WaterProgress).ttg.BeginAnimation(TranslateTransform.YProperty, da);
        }

        public WaterProgress()
        {
            InitializeComponent();
        }
    }
}
