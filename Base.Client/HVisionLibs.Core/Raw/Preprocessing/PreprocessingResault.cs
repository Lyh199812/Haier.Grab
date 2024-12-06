using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVisionLibs.Core.Preprocessing
{
    public class PreprocessingResault:BindableBase
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 耗时
        /// </summary>
        public double TimeSpan { get; set; }

        public bool IsSuccess { get; set; }
    }
}
