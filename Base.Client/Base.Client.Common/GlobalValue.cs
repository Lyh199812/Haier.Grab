using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;

namespace Base.Client.Common
{
    public class GlobalValue
    {
        public bool IsLocalMode {  get; set; }=true;
        public UserEntity UserInfo { get; set; }

    }
}
