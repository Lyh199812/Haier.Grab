using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IRecordDal
    {
        Task<string> Save(string json);
        Task<string> GetRecordByLicense(string license);
    }
}
