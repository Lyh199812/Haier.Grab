using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXC.Client.Entity
{
    public class BXCRunLogEntry
    {
        public int LogType { get; set; }
        public string LogIcon { get; set; }
        public string IconColor { get; set; }
        public DateTime LogTime { get; set; }
        public string LogInfo { get; set; }
    }

}
