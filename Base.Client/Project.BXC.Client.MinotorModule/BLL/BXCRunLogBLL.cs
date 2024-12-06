using Base.Client.BXC.IBLL;
using Base.Client.BXC.IDAL;
using Base.Client.IBLL;
using Base.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.BXC.BLL
{
    public class BXCRunLogBLL : IBXCRunLogBLL
    {
        public IBXCRunLogDAL runLogDAL {  get; set; }

        public BXCRunLogBLL(IBXCRunLogDAL runLogDAL)
        {
            this.runLogDAL = runLogDAL;
        }

        public void LogInfo(string message)
        {
            runLogDAL.AddRunLog(0, $"{message}");

        }

        public void LogWarning(string message)
        {
            runLogDAL.AddRunLog(1, $"{message}");

        }
        public void LogError(string message)
        {
            runLogDAL.AddRunLog(2, $"{message}");
        }

    }
}
