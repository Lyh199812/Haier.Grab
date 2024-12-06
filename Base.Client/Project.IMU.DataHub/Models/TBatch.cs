using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.Models
{
    [Table("t_datahub_batch")]

    public class TBatch
    {
        [Key]
        public string BatchID { get; set; } // 批次唯一标识

        /// <summary>
        /// 托盘ID
        /// </summary>
        public string TrayID { get; set; } // 关联的托盘ID

        /// <summary>
        /// 当前站点
        /// </summary>
        public string CurrentStation { get; set; } // 当前工艺站点

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 批次状态
        /// </summary>
        public string Status { get; set; } // 例如："进行中"、"已完成"、"异常"


        /// <summary>
        /// 最近更新时间
        /// </summary>
        public DateTime? LastUpdatedTime { get; set; }

        /// <summary>
        /// 批次备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 穴位条码和测试结果
        /// </summary>
        public string Position1Barcode { get; set; }
        public string Position1Result { get; set; }

        public string Position2Barcode { get; set; }
        public string Position2Result { get; set; }

        public string Position3Barcode { get; set; }
        public string Position3Result { get; set; }

        public string Position4Barcode { get; set; }
        public string Position4Result { get; set; }

        public string Position5Barcode { get; set; }
        public string Position5Result { get; set; }

        public string Position6Barcode { get; set; }
        public string Position6Result { get; set; }
    }

}
