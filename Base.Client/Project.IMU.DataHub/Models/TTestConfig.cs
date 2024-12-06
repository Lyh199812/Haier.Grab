using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.Models
{
    [Table("t_datahub_testconfig")]
    public class TTestConfig
    {
        [Category("基本信息")]
        [DisplayName("ID")]
        [Description("唯一标识符")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 设置为自增
        public int Id { get; set; }

        [Category("服务端参数设置")]
        [DisplayName("服务端 IP 地址")]
        [Description("设置连接的服务端 IP 地址，例如：192.168.1.1")]
        public string ServerIPAddress { get; set; }

        [Category("服务端参数设置")]
        [DisplayName("服务端端口号")]
        [Description("设置连接的服务端端口号，范围：0-65535")]
        public int ServerPort { get; set; }

        [Category("服务端参数设置")]
        [DisplayName("是否可编辑")]
        [Description("控制服务端设置是否可以编辑")]
        public bool IsServerEditable { get; set; } = true;

        [Category("文件路径设置")]
        [DisplayName("通过文件路径")]
        [Description("存储通过文件的路径")]
        public string PassFilePath { get; set; }


        [Category("文件路径设置")]
        [DisplayName("失败文件路径")]
        [Description("存储失败文件的路径")]
        public string FailFilePath { get; set; }


        [Category("文件路径设置")]
        [DisplayName("错误文件路径")]
        [Description("存储错误文件的路径")]
        public string ErrorFilePath { get; set; }
    }
}
