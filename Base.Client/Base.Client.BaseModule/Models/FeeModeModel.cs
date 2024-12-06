using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Common;

namespace Base.Client.BaseModule.Models
{
    public class FeeModeModel : ModelBase
    {
        public int FeeModeId { get; set; }
        private string _feeModeName;
        public string FeeModeName
        {
            get { return _feeModeName; }
            set { SetProperty(ref _feeModeName, value); }
        }

    }
}
