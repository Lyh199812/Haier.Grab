using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IUserDal
    {
        Task<string> GetAll(string key);

        Task<string> GetUsersByRole(string roleId);

        Task<string> SaveUser(string data);
        Task<string> SaveRoles(string data);

        Task<string> ResetPassword(int userId);

        Task<string> CheckUserName(string userName);
    }
}
