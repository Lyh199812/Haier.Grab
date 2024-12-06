using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IDAL;
using static Unity.Storage.RegistrationSet;

namespace Base.Client.BLL
{
    public class MenuBLL : IMenuBLL
    {
        [Dependency]
        public IMenuDal menuDal { get; set; }

        public async Task<List<MenuEntity>> GetMenus(int id)
        {
            List<MenuEntity> menuEntities = new List<MenuEntity>();

            var menus = await menuDal.GetMenu(id);// Result
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<MenuEntity>>>(menus);
            if (result.state == 200)
            {
                menuEntities = result.data;// 这个赋值是否可行？
            }
            else
                throw new Exception(result.exceptionMessage);

            return menuEntities;
        }

        public async Task<List<MenuEntity>> GetAllMenus()
        {
            var menus = await menuDal.GetAllMenus();// Result
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<MenuEntity>>>(menus);
            if (result.state == 200)
            {
                List<MenuEntity> menuEntities = result.data;// 这个赋值是否可行？
                return menuEntities;
            }
            else
                throw new Exception(result.exceptionMessage);

        }

        public async Task<ResultEntity<bool>> SaveMenu(MenuEntity menu)
        {
            string saveJson = await menuDal.SaveMenu(System.Text.Json.JsonSerializer.Serialize(menu));

            ResultEntity<bool> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<bool>>(saveJson);

            return result;
        }

        public async Task<List<int>> GetMenusByRole(int roleId)
        {
            var menus = await menuDal.GetMenusByRole(roleId);// Result
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<int>>>(menus);
            if (result.state == 200)
            {
                return result.data;
            }
            else
                throw new Exception(result.exceptionMessage);
        }
    }
}
