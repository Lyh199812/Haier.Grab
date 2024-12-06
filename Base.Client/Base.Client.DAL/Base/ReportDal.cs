using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    public class ReportDal : WebDataAccess, IReportDal
    {
        public Task<string> GetReportByDate(string startDate, string endDate)
        {
            return this.GetDatas($"api/report/{startDate}/{endDate}");
        }
    }
}
