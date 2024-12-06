using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;

namespace Base.Client.IBLL
{
    public interface IBaseBLL
    {
        Task<List<ColorEntity>> GetAutoColors();
        Task<List<ColorEntity>> GetLicenseColors();
        Task<List<FeeModeEntity>> GetFeeModes();

        Task<bool> SaveAutoColor(ColorEntity colorEntity);
        Task<bool> SaveLicenseColor(ColorEntity colorEntity);
        Task<bool> SaveFeeMode(FeeModeEntity feeModeEntity);
    }
}
