using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class AutoEntity
    {
        public int autoId { get; set; }
        public string autoLicense { get; set; }
        public int licenseColorId { get; set; }
        public string licenseColorName { get; set; }
        public int autoColorId { get; set; }
        public string autoColorName { get; set; }
        public int feeModeId { get; set; }
        public string feeModeName { get; set; }
        public string description { get; set; }
        public int state { get; set; }
        public string validStartTime { get; set; }
        public string validEndTime { get; set; }
        public int validCount { get; set; }
    }
}
