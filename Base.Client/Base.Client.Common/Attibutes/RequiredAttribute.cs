using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Common.Attibutes
{
    public class RequiredAttribute : Attribute
    {
        public string PropName { get; set; }
        public RequiredAttribute(string propName)
        {
            PropName = propName;
        }
    }
}
