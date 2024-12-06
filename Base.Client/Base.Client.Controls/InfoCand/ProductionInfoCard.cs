using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Base.Client.Controls
{
    public class ProductionInfoCard:Control,INotifyPropertyChanged
    {
        // INotifyPropertyChanged 接口的实现
        public event PropertyChangedEventHandler PropertyChanged;

        // 用于触发 PropertyChanged 事件
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Property
        // 生产数量
        public static readonly DependencyProperty ProductionCountProperty =
            DependencyProperty.Register("ProductionCount", typeof(int), typeof(ProductionInfoCard), new PropertyMetadata(0));

        public int ProductionCount
        {
            get { return (int)GetValue(ProductionCountProperty); }
            set { SetValue(ProductionCountProperty, value); OnPropertyChanged("ProductionCount"); }
        }

        // 不合格数量
        public static readonly DependencyProperty DefectiveCountProperty =
            DependencyProperty.Register("DefectiveCount", typeof(int), typeof(ProductionInfoCard), new PropertyMetadata(0));

        public int DefectiveCount
        {
            get { return (int)GetValue(DefectiveCountProperty); }
            set { SetValue(DefectiveCountProperty, value); }
        }

        // 合格率
        public static readonly DependencyProperty PassRateProperty =
            DependencyProperty.Register("PassRate", typeof(double), typeof(ProductionInfoCard), new PropertyMetadata(0.0));

        public double PassRate
        {
            get { return (double)GetValue(PassRateProperty); }
            set { SetValue(PassRateProperty, value); }
        }

        // 更新时间
        public static readonly DependencyProperty UpdateTimeProperty =
            DependencyProperty.Register("UpdateTime", typeof(string), typeof(ProductionInfoCard), new PropertyMetadata(string.Empty));

        public string UpdateTime
        {
            get { return (string)GetValue(UpdateTimeProperty); }
            set { SetValue(UpdateTimeProperty, value); }
        }

        // 开始时间
        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("StartTime", typeof(string), typeof(ProductionInfoCard), new PropertyMetadata(string.Empty));

        public string StartTime
        {
            get { return (string)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }
        #endregion


        //#region Command
        //// 添加命令依赖属性
        //public static readonly DependencyProperty RecalculateCommandProperty =
        //    DependencyProperty.Register(nameof(RecalculateCommand), typeof(ICommand), typeof(ProductionInfoCard), new PropertyMetadata(null));

        //public ICommand RecalculateCommand
        //{
        //    get => (ICommand)GetValue(RecalculateCommandProperty);
        //    set => SetValue(RecalculateCommandProperty, value);
        //}
        //#endregion
    }
}
