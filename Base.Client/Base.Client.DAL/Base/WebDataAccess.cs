using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Base.Client.Common;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    // 主要处理   请求WebApi的过程     
    public class WebDataAccess : IWebDataAccess
    {
        readonly string baseUrl = "http://localhost:22643";

        //private HttpClient httpClient = new HttpClient();

        [Dependency]
        public GlobalValue _globalValue { get; set; }
        // Post方式进行请求
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">请求的URL地址</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<string> PostDatas(string url, HttpContent content, bool isUseBase = true)
        {
            using (var client = new HttpClient())// Http   Header   Body
            {
                if (isUseBase)
                    client.BaseAddress = new Uri(baseUrl);
                // 添加鉴权Token
                if (_globalValue.UserInfo != null && !string.IsNullOrEmpty(_globalValue.UserInfo.token))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalValue.UserInfo.token);
                    // 提交数据、修改数据   需要
                    // 查询   
                }

                var resp = await client.PostAsync(url, content);
                //resp.IsSuccessStatusCode
                return await resp.Content.ReadAsStringAsync();
            }
        }

        // 字符串
        // 文件二进制
        public MultipartFormDataContent GetFormData(Dictionary<string, HttpContent> contents)
        {
            var postContent = new MultipartFormDataContent();
            string boundary = string.Format("--{0}", DateTime.Now.Ticks.ToString("x"));

            postContent.Headers.Add("ContentType", $"multipart/form-data, boundary={boundary}");

            // "{\"username\":\"admin\",\"password\":\"123456\"}"

            foreach (var item in contents)
            {
                // 需要进行传递的键值对：比如 ：username=admin
                postContent.Add(item.Value, item.Key);
            }

            return postContent;
        }

        // Get方式进行请求

        public async Task<string> GetDatas(string url, bool isUseBase = true)
        {
            using (var client = new HttpClient())
            {
                if (_globalValue.UserInfo != null && !string.IsNullOrEmpty(_globalValue.UserInfo.token))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalValue.UserInfo.token);
                }
                if (isUseBase)
                    client.BaseAddress = new Uri(baseUrl);
                var resp = client.GetAsync(url).GetAwaiter().GetResult();
                return await resp.Content.ReadAsStringAsync();
            }
        }


        public void Upload(string url, string file, Action<int> prograssChanged, Action completed, Dictionary<string, object> headers = null)
        {
            //HttpWebRequest httpWebRequest= HttpWebRequest.CreateHttp(url);
            //    分组上传    2000M   
            //  请求大小

            using (WebClient client = new WebClient())
            {
                // 添加鉴权Token
                client.Headers.Add("Authorization", "Bearer " + _globalValue.UserInfo.token);

                client.UploadProgressChanged += (se, ev) =>
                prograssChanged?.Invoke(ev.ProgressPercentage);
                client.UploadFileCompleted += (se, ev) =>
                completed?.Invoke();

                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        // 因为Value中有中文
                        client.Headers.Add(item.Key, Convert.ToBase64String(Encoding.UTF8.GetBytes(item.Value.ToString())));
                    }
                }

                client.UploadFileAsync(new Uri(baseUrl + url), "POST", file);
            }
        }
    }
}
