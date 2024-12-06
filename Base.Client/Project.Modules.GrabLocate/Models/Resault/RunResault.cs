using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Modules.GrabLocate.Models.Resault
{
    public class RunResault
    {
        public int Index { get; set; }         // 序号
        public double Row { get; set; }        // 中点坐标X
        public double Column { get; set; }     // 中点坐标Y
        public double Angle { get; set; }      // 角度
        public double Score { get; set; }      // 分数
        public double RobotX { get; set; }     // RobotX
        public double RobotY { get; set; }     // RobotY
        public double RobotAngle { get; set; } // Robot角度

    }
}
