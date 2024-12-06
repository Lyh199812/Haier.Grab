using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    public class AutoDal : WebDataAccess, IAutoDal
    {
        public Task<string> GetAll(string key, int index, int count)
        {
            Dictionary<string, HttpContent> GetFormData = new Dictionary<string, HttpContent>();

            GetFormData.Add("key", new StringContent(key));
            GetFormData.Add("index", new StringContent(index.ToString()));
            GetFormData.Add("count", new StringContent(count.ToString()));
            return this.PostDatas("api/auto/list", this.GetFormData(GetFormData));
        }
        public Task<string> GetByLicense(string license)
        {
            return this.GetDatas("api/auto/" + license);
        }
        public Task<string> SaveAuto(string autoJson)
        {
            StringContent content = new StringContent(autoJson);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return this.PostDatas("api/auto/save", content);
        }
    }
}
