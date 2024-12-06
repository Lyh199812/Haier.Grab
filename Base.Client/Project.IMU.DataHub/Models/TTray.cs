using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.Models
{
    [Table("t_datahub_tray")]
    public class TTray
    {
        [Key]

        public string TrayID { get; set; } // 托盘唯一标识

        // <summary>
        /// 使用次数
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 最近上料时间
        /// </summary>
        public DateTime? LastLoadTime { get; set; }

        /// <summary>
        /// 当前批次号
        /// </summary>
        public string CurrentBatchID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }

}
