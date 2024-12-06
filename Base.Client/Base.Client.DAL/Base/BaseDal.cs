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
    public class BaseDal : WebDataAccess, IBaseDal
    {
        public Task<string> GetAutoColors()
        {
            return this.GetDatas("api/baseinfo/autocolor");
        }

        public Task<string> GetFeeModes()
        {
            return this.GetDatas("api/baseinfo/feemode");
        }

        public Task<string> GetLicenseColors()
        {
            return this.GetDatas("api/baseinfo/licecolor");
        }

        public Task<string> SaveAutoColor(string acInfo)
        {
            StringContent content = new StringContent(acInfo);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.PostDatas("/api/baseinfo/save_auto_color", content);
        }

        public Task<string> SaveFeeModes(string feeMode)
        {
            StringContent content = new StringContent(feeMode);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.PostDatas("/api/baseinfo/save_fee_mode", content);
        }

        public Task<string> SaveLicenseColor(string lcInfo)
        {
            StringContent content = new StringContent(lcInfo);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this.PostDatas("/api/baseinfo/save_license_color", content);
        }
    }
}
