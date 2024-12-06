using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Models.Recipe
{
    public class RecipeParam
    {
        /// <summary>
        /// 温度高限
        /// </summary>
        public float TempHigh { get; set; }
        /// <summary>
        /// 温度低限
        /// </summary>
        public float TempLow { get; set; }
        /// <summary>
        /// 湿度高限
        /// </summary>
        public float HumidityHigh { get; set; }
        /// <summary>
        /// 湿度底限
        /// </summary>
        public float HumidityLow { get; set; }
        /// <summary>
        /// 是否启用温度报警
        /// </summary>
        public bool TempAlarmEnable { get; set; }
        /// <summary>
        /// 是否启用湿度报警
        /// </summary>
        public bool HumidityAlarmEnable { get; set; }
    }
}
