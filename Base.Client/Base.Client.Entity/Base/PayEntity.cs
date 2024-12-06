using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class PayEntity
    {
        public long orderId { get; set; }
        public string apiKey { get; set; }
        public string oneceId { get; set; }
        public double totalPay { get; set; }
    }
}
