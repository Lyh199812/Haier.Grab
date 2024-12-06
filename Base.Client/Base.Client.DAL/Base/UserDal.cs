using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    public class UserDal : WebDataAccess, IUserDal
    {
        public Task<string> CheckUserName(string userName)
        {
            return this.GetDatas("/api/user/check/" + userName);
        }

        public Task<string> GetAll(string key)
        {
            string url = "/api/user/all";
            if (!string.IsNullOrEmpty(key))
                url += "?key=" + key;
            return this.GetDatas(url);
        }

        public Task<string> GetUsersByRole(string roleId)
        {
            return this.GetDatas("/api/user/byrole/" + roleId);
        }

        public Task<string> ResetPassword(int userId)
        {
            Dictionary<string, HttpContent> param = new Dictionary<string, HttpContent>();
            param.Add("userId", new StringContent(userId.ToString()));

            return this.PostDatas("/api/user/resetpwd", this.GetFormData(param));
        }

        public async Task<string> SaveRoles(string data)
        {
            StringContent content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return await this.PostDatas("/api/user/save_roles", content);
        }

        public async Task<string> SaveUser(string data)
        {
            StringContent content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return await this.PostDatas("/api/user/save", content);

            //ResultEntity<bool> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<bool>>(respResult);

            //return result != null && result.state == 200;
        }
    }
}
