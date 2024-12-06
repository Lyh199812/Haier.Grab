using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class RecordEntity
    {
        public int recordId { get; set; }
        public string autoLicense { get; set; }
        public string enterTime { get; set; }
        public string leaveTime { get; set; }
        public long orderId { get; set; }
        public int feeModeId { get; set; }
        public int state { get; set; }
        public double payable { get; set; }
        public double payment { get; set; }
        public double discount { get; set; }
        public int payType { get; set; }

        // 缺少用户ID  没有跟踪用户信息
    }
}
