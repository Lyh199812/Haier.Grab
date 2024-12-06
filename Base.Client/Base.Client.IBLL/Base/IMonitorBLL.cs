using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;

namespace Base.Client.IBLL
{
    public interface IMonitorBLL
    {
        List<DeviceEntity> GetDeviceList();
    }
}
