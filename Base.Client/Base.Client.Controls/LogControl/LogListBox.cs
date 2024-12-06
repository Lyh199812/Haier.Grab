using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Base.Client.Entity;
using System.Windows.Input;
using System.Collections.Specialized;

namespace Base.Client.Controls
{
    public class LogListBox : Control
    {
        public ObservableCollection<RunLogEntity> LogMessages
        {
            get => (ObservableCollection<RunLogEntity>)GetValue(LogMessagesProperty);
            set => SetValue(LogMessagesProperty, value);
        }

        public static readonly DependencyProperty LogMessagesProperty =
            DependencyProperty.Register(nameof(LogMessages), typeof(ObservableCollection<RunLogEntity>), typeof(LogListBox), new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // 绑定双击事件
            if (GetTemplateChild("PART_Lsb") is ListBox listBox)
            {
                listBox.MouseDoubleClick += OnItemDoubleClick;
            }

            if (GetTemplateChild("PART_ClearLog") is MenuItem clearMenuItem)
            {
                clearMenuItem.Click += (s, e) => ClearLog();
            }
        }

        private void OnItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is RunLogEntity logMessage)
            {
                // 显示日志详情窗口
                var detailWindow = new Window
                {
                    Title = "日志详情",
                    Width = 400,
                    Height = 300,
                    Content = new TextBox
                    {
                        Text = $"时间: {logMessage.LogTime}\n\n信息: {logMessage.LogInfo}",
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(10),
                        IsReadOnly = true,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
                    },
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow
                };

                detailWindow.Show();
            }
        }

        private void ClearLog()
        {
            LogMessages?.Clear();
        }
    }


}
