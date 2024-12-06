using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IMenuDal
    {
        // CRUD
        // 获取菜单-》用户权限相关
        Task<string> GetMenu(int id);

        Task<string> GetAllMenus();

        Task<string> GetMenusByRole(int id);

        Task<string> SaveMenu(string menu);
    }
}
