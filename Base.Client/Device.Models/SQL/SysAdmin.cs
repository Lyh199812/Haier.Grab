using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Models.SQL
{
    public class SysAdmin
    {
        public int LoginId { get; set; }

        public string LoginName { get; set; }

        public string LoginPwd { get; set; }

        /// <summary>
        /// 参数设置
        /// </summary>
        private bool paraSet;

        public bool ParamSet
        {
            get { return paraSet; }
            set 
            {
                if (value)
                {
                    this.ParamValue = "启用";
                    paraSet = true;
                }
                else
                {
                    this.ParamValue = "禁用";
                    paraSet = false;
                }
            }
        }


        public string ParamValue
        {
            get;
            set;
        }

        /// <summary>
        /// 配方管理
        /// </summary>
        private bool recipe;

        public bool Recipe
        {
            get { return recipe; }
            set
            {
                if (value)
                {
                    RecipeValue = "启用";
                    recipe = value;
                }
                else
                {
                    RecipeValue = "禁用";
                    recipe = false;
                }
            }
        }


        public string RecipeValue
        {
            get;set;
        }


        /// <summary>
        /// 历史日志
        /// </summary>
        public string HistoryLogValue
        {
            get;set;
        }


        private bool historyLog;

        public bool HistoryLog
        {
            get { return historyLog; }
            set
            {
                if (value)
                {
                    HistoryLogValue = "启用";
                    historyLog = value;
                }
                else
                {
                    HistoryLogValue = "禁用";
                    historyLog = false;
                }
            }
        }
        /// <summary>
        /// 历史趋势
        /// </summary>
        public string HistoryTrendValue
        {
            get;set;
        }

        private bool historyTrend;

        public bool HistoryTrend
        {
            get { return historyTrend; }
            set
            {
                if (value)
                {
                    HistoryTrendValue = "启用";
                    historyTrend = value;
                }
                else
                {
                    HistoryTrendValue = "禁用";
                    historyTrend = false;
                }
            }
        }
        /// <summary>
        /// 用户管理
        /// </summary>
        public string UserManageValue
        { get; set; }

        private bool userManage;

        public bool UserManage
        {
            get { return userManage; }
            set
            {
                if (value)
                {
                    UserManageValue = "启用";
                    userManage = value;
                }
                else
                {
                    UserManageValue = "禁用";
                    userManage = false;
                }
            }
        }
    }
}
