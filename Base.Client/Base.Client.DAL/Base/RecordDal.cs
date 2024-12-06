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
    public class RecordDal : WebDataAccess, IRecordDal
    {
        public Task<string> GetRecordByLicense(string license)
        {
            return this.GetDatas("api/record/" + license);
        }

        public Task<string> Save(string json)
        {
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.PostDatas("/api/record/save", content);
        }
    }
}
