using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IPayDal
    {
        Task<string> GetPayUrl(string payJson);

        Task<string> GetPayState(long orderId);
    }
}
