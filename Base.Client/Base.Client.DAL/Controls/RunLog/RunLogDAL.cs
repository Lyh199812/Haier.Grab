using Base.Client.Entity;
using Base.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace Base.Client.DAL
{
    public class RunLogDAL : IRunLogDAL
    {
        public void AddRunLog(int type, string info, ObservableCollection<RunLogEntity> logs)
        {
            if (logs == null)
            {
                throw new ArgumentNullException(nameof(logs));
            }

            RunLogEntity runLog = new RunLogEntity
            {
                LogType = type,
                LogInfo = info,
                LogTime = DateTime.Now
            };

            switch (type)
            {
                case 0:
                    runLog.LogIcon = "\ue626";
                    runLog.IconColor = "Gray";
                    break;
                case 1:
                    runLog.LogIcon = "\ue638";
                    runLog.IconColor = "Green";
                    break;
                case 2:
                    runLog.LogIcon = "\ueb80";
                    runLog.IconColor = "Orange";
                    break;
                default:
                    runLog.LogIcon = "\ue610";
                    runLog.IconColor = "Red";
                    break;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                logs.Insert(0, runLog);

                if (logs.Count > 1000)
                {
                    logs.RemoveAt(logs.Count - 1);
                }
            });
        }

        public void CleanOldLogs(ObservableCollection<RunLogEntity> logs)
        {
            logs?.Clear();
        }

        public virtual void SaveLog(RunLogEntity log)
        {
            string logEntry = $"{log.LogTime:yyyy-MM-dd HH:mm:ss} [{log.LogType}] {log.LogInfo}";
            File.AppendAllText($"../LogFiles/{DateTime.Now.ToString()}logs.txt", logEntry + Environment.NewLine);
        }
    }



}
