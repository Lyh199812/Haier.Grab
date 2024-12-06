using System;
using System.Threading.Tasks;
using Base.Client.Entity;

namespace Base.Client.IBLL
{
    public interface ILoginBLL
    {
        Task<UserEntity> Login(string un, string pwd);
    }
}
