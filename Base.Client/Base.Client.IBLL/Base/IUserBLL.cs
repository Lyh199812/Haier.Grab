using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;

namespace Base.Client.IBLL
{
    public interface IUserBLL
    {
        Task<List<UserEntity>> GetAll(string key);

        Task<List<UserEntity>> GetUsersByRole(string roleId);

        Task<ResultEntity<bool>> SaveUser(UserEntity entity);
        Task<bool> SaveRoles(UserEntity entity);


        Task<bool> ResetPassword(int userId);
        Task<bool> CheckUserName(string userName);
    }
}
