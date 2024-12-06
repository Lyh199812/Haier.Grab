using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IRoleDal
    {
        Task<string> GetAll();

        Task<string> AddRole(string roleName);
        Task<string> Save(string roleinfo);

    }
}
