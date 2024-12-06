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
    public class RoleDal : WebDataAccess, IRoleDal
    {
        // 接收的对象的Json字符串
        public Task<string> AddRole(string roleDataJson)
        {
            StringContent content = new StringContent(roleDataJson);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return this.PostDatas("/api/role/save", content);
        }

        public Task<string> GetAll()
        {
            return this.GetDatas("/api/role/all");
        }


        public Task<string> Save(string roleinfo)
        {
            StringContent content = new StringContent(roleinfo);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return this.PostDatas("/api/role/save_role", content);
        }
    }
}
