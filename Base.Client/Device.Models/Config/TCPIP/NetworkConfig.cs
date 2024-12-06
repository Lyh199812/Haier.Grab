using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Models.Config.TCPIP
{
    public class NetworkConfig
    {
        public string ProtocolType { get; set; } = "TCPServer"; // 支持 TCPServer, TCPClient, UDP
        public string LocalIP { get; set; } = "127.0.0.1";
        public int LocalPort { get; set; } = 8080;
        public string RemoteIP { get; set; }
        public int RemotePort { get; set; }
    }

}
