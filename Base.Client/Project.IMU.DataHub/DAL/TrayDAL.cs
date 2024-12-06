using Base.Client.DAL;
using Base.Client.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.DAL
{
    public class TrayDAL : BaseDBService
    {
        public TrayDAL(IMUDataHubDBConfig dbConfig) :base(dbConfig)
        {
            
        }
    }
}
