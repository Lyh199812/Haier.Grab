using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    public class MenuDal : WebDataAccess, IMenuDal
    {
        /// <summary>
        /// 用来获取系统所提供的所有菜单项
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllMenus()
        {
            string menuJson = await this.GetDatas("api/menu/all");
            return menuJson;
        }

        public async Task<string> GetMenu(int id)
        {
            // 根据当前用户来获取对应的菜单
            // 这个逻辑后续会做修改，需要匹配权限
            string menuJson = await this.GetDatas("api/menu/all");
            return menuJson;
        }

        public Task<string> GetMenusByRole(int id)
        {
            return this.GetDatas("api/menu/byrole/" + id.ToString());
        }

        public async Task<string> SaveMenu(string menu)
        {
            StringContent content = new StringContent(menu);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return await this.PostDatas("/api/menu/save", content);
        }
    }
}
