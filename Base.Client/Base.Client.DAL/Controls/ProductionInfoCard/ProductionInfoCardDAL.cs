using Base.Client.EFCore;
using Base.Client.Entity;
using Base.Client.Entity.Message;
using Base.Client.IDAL;
using Base.Client.IDAL.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.DAL.Controls.ProductionInfoCard
{
    public class ProductionInfoCardDAL:BaseDBService, IProductionInfoCardDAL
    {
        public ProductionInfoCardDAL(BaseDBConfig baseDAL) : base(baseDAL)
        {
        }

       
    }
}
