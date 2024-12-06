using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.Common.Attibutes;

namespace Base.Client.Common
{
    public class ModelBase : BindableBase, IDataErrorInfo
    {
        // 只有一个特性检查
        // 需要把所有未通过的检查都提示   List记录所有检查异常    拼接成字符串返回
        // 遇到什么问题就提示什么问题

        // 当返回True表示数据库中已存在
        //       False表示不存在
        public Func<object, bool> UniquenessCheck { get; set; }

        // 每个实例的属性索引器，根据属性名称来索引
        public string this[string columnName]
        {
            get
            {
                // 微软徽标+V  ：  剪切板
                // 1、无区别的进行检查
                //if(columnName == "UserName")
                // 2、无区别的进行消息提示

                // 字符串获取属性内容：反射
                PropertyInfo pi = this.GetType().GetProperty(columnName);

                var value = pi.GetValue(this, null);
                if (pi.IsDefined(typeof(RequiredAttribute), true))
                {
                    if (value == null || string.IsNullOrEmpty(value.ToString()))
                        return pi.GetCustomAttribute<RequiredAttribute>().PropName = "不能为空";
                }
                if (pi.IsDefined(typeof(UniquenessAttribute), true))
                {
                    // 判断会是   
                    if (UniquenessCheck?.Invoke(value) == true)
                    {
                        // 提示错误信息  
                        return pi.GetCustomAttribute<UniquenessAttribute>().ErrorMessage;// 不合理
                    }
                }

                return "";
            }
        }

        public string Error => null;


        public int Index { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
    }
}
