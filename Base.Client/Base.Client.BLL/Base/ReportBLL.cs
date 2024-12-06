using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IDAL;

namespace Base.Client.BLL
{
    public class ReportBLL : IReportBLL
    {
        IReportDal _reportDal;
        public ReportBLL(IReportDal reportDal)
        {
            _reportDal = reportDal;
        }
        public async Task<List<ReportEntity>> GetReportByDate(string startDate, string endDate)
        {
            string dataJson = await _reportDal.GetReportByDate(startDate, endDate);
            ResultEntity<List<ReportEntity>> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<ReportEntity>>>(dataJson);

            if (result != null)
            {
                if (result.state == 200)
                    return result.data;
                else
                    throw new Exception(result.exceptionMessage);
            }
            else
                throw new Exception("获取数据异常");
        }
    }
}
