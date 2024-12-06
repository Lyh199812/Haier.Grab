using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Events.TCPIP
{
    public class ClientEventArgs : EventArgs
    {
        public string ClientId { get; }

        public ClientEventArgs(string clientId)
        {
            ClientId = clientId;
        }
    }

}
