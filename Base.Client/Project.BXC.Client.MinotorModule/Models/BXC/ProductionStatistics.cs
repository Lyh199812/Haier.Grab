using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity.BXC
{
    public class ProductionStatistics
    {
        public int Id { get; set; } // 唯一标识符
        //public string ProductId { get; set; } // 产品编号
        //public string ProductName { get; set; } // 产品名称
        //public string BatchNumber { get; set; } // 批次号
        public DateTime ProductionDate { get; set; } // 生产日期
        public int TotalProduced { get; set; } // 生产总数
        public int NumScanFail { get; set; } // 生产总数
        
        public int QualifiedQuantity { get; set; } // 合格数量
        public int UnqualifiedQuantity { get; set; } // 不合格数量
        public string Remarks { get; set; } // 备注或附加信息

        [NotMapped]
        public decimal QualifiedRate
        {
            get
            {
                return TotalProduced > 0 ? Math.Round((decimal)QualifiedQuantity / TotalProduced * 100, 3) : 0;
            }
        } // 合格率（百分比）
    }
}
