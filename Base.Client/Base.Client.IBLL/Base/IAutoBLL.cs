using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;

namespace Base.Client.IBLL
{
    public interface IAutoBLL
    {
        Task<PaginationResult<List<AutoEntity>>> GetAll(string key, int index, int count);
        Task<AutoEntity> GetByLicense(string license);
        Task<bool> SaveAuto(AutoEntity entity);
    }
}
