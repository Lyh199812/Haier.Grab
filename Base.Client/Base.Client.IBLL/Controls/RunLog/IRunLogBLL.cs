using Base.Client.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IBLL
{
    public interface IRunLogBLL
    {
        ObservableCollection<RunLogEntity> Logs { get;set; }

        /// <summary>
        /// 获取所有运行日志
        /// </summary>
        ObservableCollection<RunLogEntity> GetAllLogs();

        /// <summary>
        /// 添加信息日志
        /// </summary>
        /// <param name="info">日志内容</param>
        void AddInfoLog(string info);

        /// <summary>
        /// 添加执行成功日志
        /// </summary>
        /// <param name="info">日志内容</param>
        void AddSuccessLog(string info);

        /// <summary>
        /// 添加警告日志
        /// </summary>
        /// <param name="info">日志内容</param>
        void AddWarningLog(string info);

        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="info">日志内容</param>
        void AddErrorLog(string info);

        /// <summary>
        /// 清除所有旧日志
        /// </summary>
        void ClearAllLogs();
    }
}
