using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    public class LoginDal :  ILoginDal
    {
        IWebDataAccess _webDataAccess;
        public LoginDal(IWebDataAccess webDataAccess)
        {
            _webDataAccess = webDataAccess;
        }
        public Task<string> Login(string username, string password)
        {
            // 请求数据接口API   Post
            // HttpContent  键值对

            Dictionary<string, HttpContent> GetFormData = new Dictionary<string, HttpContent>();

            GetFormData.Add("username", new StringContent(username));
            GetFormData.Add("password", new StringContent(password));
            return _webDataAccess.PostDatas("api/login", _webDataAccess.GetFormData(GetFormData));
        }
    }
}
