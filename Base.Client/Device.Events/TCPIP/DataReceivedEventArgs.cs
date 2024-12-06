using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Events.TCPIP
{
    public class DataReceivedEventArgs : EventArgs
    {
        public string Message { get; }
        public string SenderIpAddress { get; }

        public DataReceivedEventArgs(string message, string senderIpAddress)
        {
            Message = message;
            SenderIpAddress = senderIpAddress;
        }
    }
}
