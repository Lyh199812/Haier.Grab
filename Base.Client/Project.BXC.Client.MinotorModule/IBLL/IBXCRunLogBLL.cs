using Base.Client.BXC.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.BXC.IBLL
{
    public interface IBXCRunLogBLL
    {
        IBXCRunLogDAL runLogDAL { get; set; }
        public void LogInfo(string message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}
