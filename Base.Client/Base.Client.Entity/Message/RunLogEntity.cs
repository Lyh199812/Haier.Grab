using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class RunLogEntity
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }=DateTime.Now;
        public int LogType { get; set; } = 0;
        public string LogInfo { get; set; }
        public string LogIcon { get; set; } = "\ue62b";

        public string IconColor { get; set; } = "Gray";
    }

}
