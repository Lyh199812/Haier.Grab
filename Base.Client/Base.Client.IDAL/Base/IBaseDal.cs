using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IBaseDal
    {
        Task<string> GetAutoColors();
        Task<string> GetLicenseColors();
        Task<string> GetFeeModes();

        Task<string> SaveAutoColor(string acInfo);
        Task<string> SaveLicenseColor(string lcInfo);
        Task<string> SaveFeeModes(string feeMode);
    }
}
