using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IDAL;
using static Unity.Storage.RegistrationSet;

namespace Base.Client.BLL
{
    public class UserBLL : IUserBLL
    {
        IUserDal _userDal;
        public UserBLL(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<List<UserEntity>> GetAll(string key)
        {
            string userJson = await _userDal.GetAll(key);

            ResultEntity<List<UserEntity>> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<UserEntity>>>(userJson);

            if (result.state == 200)
                return result.data;
            else
                throw new Exception(result.exceptionMessage);
        }

        public async Task<List<UserEntity>> GetUsersByRole(string roleId)
        {
            string userJson = await _userDal.GetUsersByRole(roleId);

            ResultEntity<List<UserEntity>> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<UserEntity>>>(userJson);

            if (result.state == 200)
                return result.data;
            else
                throw new Exception(result.exceptionMessage);
        }

        public async Task<bool> ResetPassword(int userId)
        {
            string json = await _userDal.ResetPassword(userId);
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

        public async Task<bool> SaveRoles(UserEntity entity)
        {
            string json = await _userDal.SaveRoles(System.Text.Json.JsonSerializer.Serialize(entity));
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

        public async Task<ResultEntity<bool>> SaveUser(UserEntity entity)
        {
            string saveJson = await _userDal.SaveUser(System.Text.Json.JsonSerializer.Serialize(entity));

            ResultEntity<bool> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<bool>>(saveJson);

            return result;
        }

        public async Task<bool> CheckUserName(string userName)
        {
            var result = await _userDal.CheckUserName(userName);
            return bool.Parse(result);
        }
    }
}
