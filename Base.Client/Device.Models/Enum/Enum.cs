using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Models.Enum
{
    /// <summary>
    /// 存储区
    /// </summary>
    public enum StoreArea
    {
        输出线圈 = 0,
        输入线圈 = 1,
        输入寄存器 = 3,
        输出寄存器 = 4
    }

    /// <summary>
    /// 所有窗体的枚举，小于临界窗体为固定窗体
    /// </summary>
    public enum FormNames
    {
        集中监控,
        临界窗体,
        参数设置,
        配方管理,
        历史趋势,
        报警追溯,
        用户管理,
    }

    public enum TCPIPProtocolsEnum
    {
        TCPServer,
        TCPClient,
        UDP
    }
}
