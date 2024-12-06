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
    public class RecordBLL : IRecordBLL
    {
        IRecordDal _recordDal;
        public RecordBLL(IRecordDal recordDal)
        {
            _recordDal = recordDal;
        }

        public async Task<RecordEntity> GetRecordByLicense(string license)
        {
            var strJson = await _recordDal.GetRecordByLicense(license);
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<RecordEntity>>(strJson);
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

        public async Task<bool> Save(RecordEntity recordEntity)
        {
            //RecordEntity recordEntity = new RecordEntity()
            //{
            //    AutoLicense = license,
            //    EnterTime = enterTime
            //};
            string saveJson = await _recordDal.Save(System.Text.Json.JsonSerializer.Serialize(recordEntity));
            ResultEntity<bool> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<bool>>(saveJson);
            return result.state == 200;
        }
    }
}
