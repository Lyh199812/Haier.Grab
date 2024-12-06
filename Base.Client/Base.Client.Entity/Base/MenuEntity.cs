using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class MenuEntity
    {
        public int menuId { get; set; }
        public string menuHeader { get; set; }
        public string targetView { get; set; }
        public int parentId { get; set; }

        public string menuIcon { get; set; }
        public int index { get; set; }
        public int menuType { get; set; }
        public int state { get; set; }
    }
}
