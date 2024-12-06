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
    public class RoleBLL : IRoleBLL
    {
        IRoleDal _roleDal;
        public RoleBLL(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        public async Task<bool> AddRole(RoleEntity role)
        {
            string json = await _roleDal.AddRole(System.Text.Json.JsonSerializer.Serialize(role));
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<bool>>(json);
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

        public async Task<List<RoleEntity>> GetAll()
        {
            string json = await _roleDal.GetAll();
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<RoleEntity>>>(json);
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

        public async Task<bool> SaveRoleInfo(RoleEntity role)
        {
            // 调用 接口
            string json = await _roleDal.Save(System.Text.Json.JsonSerializer.Serialize(role));
            // 解析返回状态 
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<bool>>(json);
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