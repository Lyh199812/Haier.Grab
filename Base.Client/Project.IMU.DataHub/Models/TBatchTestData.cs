using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.Models
{
    [Table("t_datahub_batch_testdata")]
    public class BatchTestData
    {
        /// <summary>
        /// 主键，唯一标识一条测试记录
        /// </summary>
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 关联批次表的批次ID
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string BatchID { get; set; }

        /// <summary>
        /// 测试站点名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Station { get; set; }

        /// <summary>
        /// 穴位编号（1-6）
        /// </summary>
        [Required]
        public int PositionIndex { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        [MaxLength(100)]
        public string Barcode { get; set; }

        /// <summary>
        /// 测试项名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string TestItem { get; set; }

        /// <summary>
        /// 测试结果
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string TestResult { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }
    }


}
