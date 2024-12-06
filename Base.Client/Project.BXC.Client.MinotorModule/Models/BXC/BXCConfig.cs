using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity.BXC
{
    public class BXCConfig
    {
        [Description("Id"), Category("分组1")]
        public int Id {  get; set; }

        [Description("产品名称"), Category("分组1")]
        public string ProductName {  get; set; }
        [Description("是否被选中"), Category("分组1")]
        public int IsSelected { get; set; }

        [Description("运行模式：\r\n本地模式:不进行MES过站\r\n联网模式：使用MES"), Category("分组1")]
        public string Mode { get; set; }         // 运行模式
        [Description("顶部相机IP地址")]
        public string CamIP_Top { get; set; }
        [Description("左测相机IP地址")]
        public string CamIP_Left { get; set; }

        public string EMP_NO {  get; set; }
        public string TERMINAL_ID { get; set; }

        public double MaxWeight {  get; set; }
        public double MinWeight {  get; set; }
        public int ScanDelay { get; set; }
        public int RealTimDataCount { get; set; }
        
        [Description("称重等待时间 单位ms")]
        public int WeightWaitingTime { get; set; }


    }
}
