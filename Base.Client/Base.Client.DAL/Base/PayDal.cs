using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    public class PayDal : WebDataAccess, IPayDal
    {
        public Task<string> GetPayState(long orderId)
        {
            return this.GetDatas("http://116.62.218.216/api/Pay/state/" + orderId.ToString(), false);
        }

        public Task<string> GetPayUrl(string payJson)
        {
            StringContent content = new StringContent(payJson);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return this.PostDatas("http://116.62.218.216/api/zhaoxi/pay", content, false);
        }
    }
}
