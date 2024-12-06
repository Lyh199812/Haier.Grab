using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Device.Models.Config;

namespace Device.Models.Config
{
    public class DeviceInfo
    {
        //IP地址
        public string IPAddress { get; set; }

        //端口
        public int Port { get; set; }

        //当前配方
        public string CurrentRecipe { get; set; }
        //是否连接
        public bool IsConnected { get; set; }

        //连接状态
        public bool ReConnectSign { get; set; }

        //连接时间
        public int ReConnectTime { get; set; } = 2000;

        public int GroupInterval { get; set; } = 20;

        //组列表
        public List<DeviceGroup> GroupList { get; set; }

        //当前变量的字典集合
        public Dictionary<string, object> CurrentValue = new Dictionary<string, object>();

        //触发报警属性
        public event Action<bool, DeviceVariable> AlarmTrigEvent;

        /// <summary>
        /// 更新变量
        /// </summary>
        /// <param name="variable"></param>
        public void UpdateVariable(DeviceVariable variable)
        {
            if (CurrentValue.ContainsKey(variable.VarName))
            {
                CurrentValue[variable.VarName] = variable.VarValue;
            }
            else
            {
                CurrentValue.Add(variable.VarName, variable.VarValue);
            }
        }

        /// <summary>
        /// 检查报警
        /// </summary>
        /// <param name="variable"></param>
        private void CheckAlarm(DeviceVariable variable)
        {
            if (variable.PosAlarm)
            {
                bool currentValue = variable.VarValue.ToString() == "True";
                if (!variable.PosCacheValue && currentValue)
                {
                    AlarmTrigEvent?.Invoke(true, variable);
                }
                if (variable.PosCacheValue && !currentValue)
                {
                    AlarmTrigEvent?.Invoke(false, variable);
                }
                variable.PosCacheValue = currentValue;
            }
            if (variable.NegAlarm)
            {
                bool currentValue = variable.VarValue.ToString() == "True";
                if (variable.NegCacheValue && !currentValue)
                {
                    AlarmTrigEvent?.Invoke(true, variable);
                }
                if (!variable.NegCacheValue && currentValue)
                {
                    AlarmTrigEvent?.Invoke(false, variable);
                }
                variable.NegCacheValue = currentValue;
            }
        }

        public object this[string key]
        {
            get
            {
                if (CurrentValue.ContainsKey(key))
                {
                    return CurrentValue[key];
                }
                else
                {
                    return null;
                }
            }
        }

    }

   
}
