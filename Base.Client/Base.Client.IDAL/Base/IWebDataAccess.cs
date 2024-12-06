using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IWebDataAccess
    {
        Task<string> PostDatas(string url, HttpContent content, bool isUseBase = true);
        MultipartFormDataContent GetFormData(Dictionary<string, HttpContent> contents);
        Task<string> GetDatas(string url, bool isUseBase = true);

        void Upload(string url, string file, Action<int> prograssChanged, Action completed, Dictionary<string, object> headers = null);
    }
}
