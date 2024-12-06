using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;

namespace Base.Client.IBLL
{
    public interface IMenuBLL
    {
        Task<List<MenuEntity>> GetMenus(int id);

        Task<List<MenuEntity>> GetAllMenus();

        Task<List<int>> GetMenusByRole(int roleId);

        Task<ResultEntity<bool>> SaveMenu(MenuEntity menu);
    }
}
