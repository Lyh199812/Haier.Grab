using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IAutoDal
    {
        Task<string> GetAll(string key, int index, int count);
        Task<string> GetByLicense(string license);

        Task<string> SaveAuto(string autoJson);
    }
}
