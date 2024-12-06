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
    public class AutoBLL : IAutoBLL
    {
        IAutoDal _autoDal;
        public AutoBLL(IAutoDal autoRegisterDal)
        {
            _autoDal = autoRegisterDal;
        }
        public async Task<PaginationResult<List<AutoEntity>>> GetAll(string key, int index, int count)
        {
            string resultStr = await _autoDal.GetAll(key, index, count);
            return System.Text.Json.JsonSerializer.Deserialize<PaginationResult<List<AutoEntity>>>(resultStr);
        }
        public async Task<AutoEntity> GetByLicense(string license)
        {
            string resultJson = await _autoDal.GetByLicense(license);
            ResultEntity<AutoEntity> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<AutoEntity>>(resultJson);
            if (result != null)
            {
                if (result.state == 200)
                    return result.data;
                else
                    return null;// 所有服务端异常都当作没有打到登记信息
            }
            else
                throw new Exception("保存数据异常");
        }
        public async Task<bool> SaveAuto(AutoEntity entity)
        {
            string autoJson = System.Text.Json.JsonSerializer.Serialize(entity);
            string resultStr = await _autoDal.SaveAuto(autoJson);
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<bool>>(resultStr);
            if (result != null)
            {
                if (result.state == 200)
                    return result.data;
                else
                    throw new Exception(result.exceptionMessage);
            }
            else
                throw new Exception("保存数据异常");
        }
    }
}
