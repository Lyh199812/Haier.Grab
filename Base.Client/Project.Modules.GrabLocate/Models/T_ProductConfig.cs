using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Modules.GrabLocate.Models
{
    public class T_ProductConfig
    {
        // 使用构造函数设置默认值
        public T_ProductConfig()
        {
            // 设置默认值
            ModelPath = string.Empty;  // 默认空字符串
            MinScore = 0.0;            // 默认最小分数为 0.0
        }

        // 设置 Id 为主键，通常是自动生成的自增值
        [Key]
        [ReadOnly(true)]  // 如果 Id 不需要修改，设置为只读
        [Description("产品配置的唯一标识符")]
        [Category("基本信息")]
        public int Id { get; set; }

        [Description("配方名称")]
        [Category("基本信息")]
        public string  Name { get; set; }

        [Description("是否被选中")]
        [Category("基本信息")]
        public bool IsSelected { get; set; }

        [Description("是否激活")]
        [Category("基本信息")]
        public bool IsActive { get; set; }
        [Description("产品抓取宽度")]
        [Category("基本信息")]
        public double GrbWidth { get; set; }


        [Description("关联型号")]
        [Category("基本信息")]
        public string AssociatedModel { get; set; } = "";

        // 模型路径
        [Description("模型文件的路径")]
        [Category("搜索")]
        public string ModelPath { get; set; }

        // 最小分数
        [Description("搜索过程中用于评估模型的最低分数")]
        [Category("搜索")]
        public double MinScore { get; set; }
        // 最小分数
        [Description("对搜索出来的模型再做一次分数筛选，一般比MinScore高;目的是防止换产误识别")]
        [Category("搜索")]
        public double MinScoreForCheck { get; set; }

        [Description("目标产品数量")]
        [Category("搜索")]
        public int TargetCount { get; set; }



        [Description("X方向偏移")]
        [Category("定位")]
        public double OffsetX { get; set; }

        [Description("Y方向偏移")]
        [Category("定位")]
        public double OffsetY { get; set; }

        [Description("角度偏移")]
        [Category("定位")]
        public double OffsetR { get; set; }

        //[Description("是否启用支撑棍位置检测")]
        //[Category("附加检测项目")]
        //public bool SupportRodPositionDetectionEnabled { get; set; }
    }
}
