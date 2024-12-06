using Device.CommLab.ViewModels;
using Device.Models.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
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

namespace Device.CommLab.Views
{
    /// <summary>
    /// NetworkAssistsView.xaml 的交互逻辑
    /// </summary>
    public partial class NetworkAssistsView : UserControl
    {
        public NetworkAssistsView(IEventAggregator eventAggregator, TCPIPProtocolsEnum _protocolType)
        {
            InitializeComponent();
            DataContext = new NetworkAssistsViewModel(eventAggregator);
        }

    }
}
