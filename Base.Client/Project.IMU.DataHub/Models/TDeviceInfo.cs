using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.Models
{
    public class TDeviceInfo
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string ClientId { get; set; }
        public string State { get; set; }
        public string StationNo { get; set; }
        public string StationName { get; set; }
        public DateTime ConnectionStartTime {  get; set; }
        public DateTime ConnectionEndTime {  get; set; }
    }
}
