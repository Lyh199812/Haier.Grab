using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class UserEntity
    {
        //"userId": 3,
        //"userName": "admin",
        //"password": "ABFB5D41B5DCCF7B34A90F32EC475E77",
        //"userIcon": "image/show/1001.png",
        //"realName": "Administrator",
        //"age": 30,
        //"state": 1
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string userIcon { get; set; }
        public string realName { get; set; }
        public int age { get; set; }
        public int state { get; set; }
        public int viewType { get; set; }
        public string token { get; set; }

        public List<MenuEntity> menus { get; set; }
        public List<RoleEntity> roles { get; set; }
    }
}
