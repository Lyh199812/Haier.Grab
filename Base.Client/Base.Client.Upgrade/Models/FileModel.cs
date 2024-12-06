using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Upgrade.Models
{
    public class FileModel : BindableBase
    {
        // 界面中有些列表需要有序号
        public int Index { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileMd5 { get; set; }
        public int FileLenght { get; set; }

        private string _state;

        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private int _progress;

        public int Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        private string _errorMsg;

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { SetProperty(ref _errorMsg, value); }
        }
    }
}
