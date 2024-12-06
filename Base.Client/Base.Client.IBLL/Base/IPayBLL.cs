using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IBLL
{
    public interface IPayBLL
    {
        Task<string> GetPayUrl(long orderId, double payment);
        Task<bool> GetPayState(long orderId);
    }
}
