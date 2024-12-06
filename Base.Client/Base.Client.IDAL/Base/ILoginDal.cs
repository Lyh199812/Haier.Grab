using System;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface ILoginDal
    {
        Task<string> Login(string username, string password);
    }
}
