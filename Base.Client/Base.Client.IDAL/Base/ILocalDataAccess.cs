using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface ILocalDataAccess
    {
        DataTable GetFileList();
        DataTable GetDeivceList();
    }
}
