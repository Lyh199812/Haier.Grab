using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class RoleEntity
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public int state { get; set; }

        public List<int> userIds { get; set; }
        public List<int> menuIds { get; set; }
    }
}
