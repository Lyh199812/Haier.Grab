using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Project.Modules.GrabLocate.Models
{
    public class T_CommonConfig
    {
        // 使用构造函数设置默认值
        public T_CommonConfig()
        {
            // 设置默认值
            RobotBaseX = 0f;
            RobotBaseY = 0f;
            RobotBaseAngle = 0f;
            ImageBaseX = 0f;
            ImageBaseY = 0f;
            ImageBaseAngle = 0f;
        }
        // 设置 Id 为只读，并为属性添加描述和分组
        [Key]
        [ReadOnly(true)]
        [Description("唯一标识符")]
        [Category("0.基本信息")]
        public int Id { get; set; }

        [Description("是否自动切换配方")]
        [Category("0.基本信息")]
        public bool AutoSwitchRecipeEnabled { get; set; }

        // 机械手基准点
        [Description("机械手基准点 X 坐标")]
        [Category("机械手设置")]
        public float RobotBaseX { get; set; }

        [Description("机械手基准点 Y 坐标")]
        [Category("机械手设置")]
        public float RobotBaseY { get; set; }

        [Description("机械手基准点角度")]
        [Category("机械手设置")]
        public float RobotBaseAngle { get; set; }

        // 图像基准点
        [Description("图像基准点 X 坐标")]
        [Category("图像设置")]
        public float ImageBaseX { get; set; }

        [Description("图像基准点 Y 坐标")]
        [Category("图像设置")]
        public float ImageBaseY { get; set; }

        [Description("图像基准点角度")]
        [Category("图像设置")]
        public float ImageBaseAngle { get; set; }

        [Description("生成一个矩形，点1的X坐标")]
        [Category("空盘校验")]
        public float ROI_X1 { get; set; }

        [Description("生成一个矩形，点1的Y坐标")]
        [Category("空盘校验")]
        public float ROI_Y1 { get; set; }

        [Description("生成一个矩形，点2的X坐标")]
        [Category("空盘校验")]
        public float ROI_X2 { get; set; }

        [Description("生成一个矩形，点2的Y坐标")]
        [Category("空盘校验")]
        public float ROI_Y2 { get; set; }

        [Description("阈值分割-灰度下限")]
        [Category("空盘校验")]
        public int ROI_MinGray { get; set; }

        [Description("阈值分割-灰度上限")]
        [Category("空盘校验")]
        public int ROI_MaxGray { get; set; }


        [Description("空盘的区域面积上限")]
        [Category("空盘校验")]
        public float ROI_MaxArea { get; set; }

    }
}
