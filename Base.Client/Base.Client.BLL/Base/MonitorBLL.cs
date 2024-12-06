using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IDAL;

namespace Base.Client.BLL
{
    public class MonitorBLL : IMonitorBLL
    {
        ILocalDataAccess _localDataAccess;
        public MonitorBLL(ILocalDataAccess localDataAccess)
        {
            _localDataAccess = localDataAccess;
        }
        public List<DeviceEntity> GetDeviceList()
        {
            DataTable datas = _localDataAccess.GetDeivceList();
            return datas.AsEnumerable().Select(row => new DeviceEntity
            {
                DeviceId = int.Parse(row["device_id"].ToString()),
                DeviceName = row["device_name"].ToString(),
                IP = row["ip"].ToString(),
                Port = int.Parse(row["port"].ToString())
            }).ToList();
        }
    }
}
