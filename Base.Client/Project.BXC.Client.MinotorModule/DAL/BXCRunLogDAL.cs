using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Windows;
using Base.Client.Entity;
using Base.Client.BXC.IDAL;
using BXC.Client.Entity;

namespace BXC.Client.DAL
{
    public class BXCRunLogDAL : BindableBase, IBXCRunLogDAL
    {
        //日志集合
        private ObservableCollection<BXCRunLogEntry> _logs;

        public ObservableCollection<BXCRunLogEntry> Logs
        {
            get => _logs;
            set
            {
                if (_logs != value)
                {
                    // 如果有旧的集合，取消订阅事件
                    if (_logs != null)
                    {
                        _logs.CollectionChanged -= Logs_CollectionChanged;
                    }

                    _logs = value;

                    // 订阅新的集合的 CollectionChanged 事件
                    if (_logs != null)
                    {
                        _logs.CollectionChanged += Logs_CollectionChanged;
                    }

                    // 通知 Logs 属性本身发生了变化
                    RaisePropertyChanged();
                }
            }
        }

        private void Logs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // 集合中的元素变化时，触发属性变更通知
            RaisePropertyChanged(typeof(BXCRunLogDAL).ToString());
        }



        public void AddRunLog(int type, string info)
        {
            if (Logs == null)
            {
                Logs = new ObservableCollection<BXCRunLogEntry>();
            }
            BXCRunLogEntry runLog = new BXCRunLogEntry() { LogType = type, LogInfo = info, LogTime = DateTime.Now };
            if (type == 0)
            {
                runLog.LogIcon = "\ue626";
                runLog.IconColor = "Green";
            }
            else if (type == 1)
            {
                runLog.LogIcon = "\ue616";
                runLog.IconColor = "Yellow";
            }
            else if (type == 2)
            {
                runLog.LogIcon = "\ue62a";
                runLog.IconColor = "red";
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                Logs.Insert(0, runLog);

            });
        }

        public void CleanOldLogs()
        {
            Logs = new ObservableCollection<BXCRunLogEntry>();
        }



    }


}
