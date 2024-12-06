using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IReportDal
    {
        // yyyyMMddHHmmss
        Task<string> GetReportByDate(string startDate, string endDate);
    }
}
