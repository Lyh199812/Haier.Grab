using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity.BXC
{
    public class BXCRecordEntity
    {
        public int Id { get; set; }           // 序号
        public string SN { get; set; }           // SN (序列号)
        public DateTime Time { get; set; }       // 时间
        public double Weight { get; set; }       // 重量
        public double MaxWeight { get; set; }       // 重量
        public double MinWeight { get; set; }       // 重量
        public string Mode { get; set; }   
        // 运行模式
        public string TestResult { get; set; }   // 测试结果

        public string ErrorInfo { get; set; } = "";
        public string MESSendMsg { get; set; } = "";
        public string MESRecMsg { get; set; } = "";
        [NotMapped]
        public string FullErrorInfo {  get; set; }
    }
}
