using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Common.Attibutes
{
    public class UniquenessAttribute : Attribute
    {
        public string ErrorMessage { get; set; }

        public UniquenessAttribute(string msg)
        {
            ErrorMessage = msg;
        }
    }
}
