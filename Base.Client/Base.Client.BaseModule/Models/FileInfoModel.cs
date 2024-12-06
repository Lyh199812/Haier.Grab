using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Base.Client.BaseModule.Models
{
    public class FileInfoModel : BindableBase
    {
        public int Index { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadTime { get; set; }

        public ICommand DeleteCommand { get; set; }
        public string FullPath { get; set; }
        private string _state;

        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }
    }
}
