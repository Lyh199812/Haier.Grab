using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity.Message
{
    public class ProductionInfoCardModel
    {
        public int Id { get; set; } // 主键
        public int ProductionCount { get; set; } // 生产数量
        public int DefectiveCount { get; set; } // 不合格数量
        public double PassRate { get; set; } = 100;

        // 使用 DateTime 类型优化时间字段
        public DateTime UpdateTime { get; set; } // 更新时间
        public DateTime StartTime { get; set; } // 开始时间
    }
}
