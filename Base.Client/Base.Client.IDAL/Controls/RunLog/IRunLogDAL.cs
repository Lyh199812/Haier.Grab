using Base.Client.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{

    public interface IRunLogDAL
    {
        /// <summary>
        /// 添加运行日志到指定集合
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="info">日志内容</param>
        /// <param name="logs">日志集合</param>
        void AddRunLog(int type, string info, ObservableCollection<RunLogEntity> logs);

        /// <summary>
        /// 清空指定的日志集合
        /// </summary>
        /// <param name="logs">日志集合</param>
        void CleanOldLogs(ObservableCollection<RunLogEntity> logs);

        /// <summary>
        /// 保存日志到持久化存储（默认实现为本地文件）
        /// </summary>
        /// <param name="log">日志实体</param>
        void SaveLog(RunLogEntity log);
    }

}
