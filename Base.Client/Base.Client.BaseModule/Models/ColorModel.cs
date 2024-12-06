using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Common;

namespace Base.Client.BaseModule.Models
{
    public class ColorModel : ModelBase
    {
        public int ColorId { get; set; }
        private string _colorName;
        public string ColorName
        {
            get { return _colorName; }
            set { SetProperty(ref _colorName, value); }
        }

    }
}
