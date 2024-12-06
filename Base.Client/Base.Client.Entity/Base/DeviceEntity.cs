using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class DeviceEntity
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
    }
}
