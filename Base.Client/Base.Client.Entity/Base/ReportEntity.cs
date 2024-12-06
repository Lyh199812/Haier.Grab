using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class ReportEntity
    {
        public string leaveTime { get; set; }
        public int orderCount { get; set; }
        public double payable { get; set; }
        public double paymentCash { get; set; }
        public double paymentElec { get; set; }
    }
}
