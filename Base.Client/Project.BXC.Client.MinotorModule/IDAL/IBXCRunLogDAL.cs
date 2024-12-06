using Base.Client.Entity;
using BXC.Client.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.BXC.IDAL
{
    public interface IBXCRunLogDAL
    {
        ObservableCollection<BXCRunLogEntry> Logs { get; set; }
        void AddRunLog(int type, string info);
        void CleanOldLogs();
    }
}
