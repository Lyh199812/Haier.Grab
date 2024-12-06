using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IDAL;

namespace Base.Client.BLL
{
    public class PayBLL : IPayBLL
    {
        IPayDal _payDal;
        public PayBLL(IPayDal payDal)
        {
            _payDal = payDal;
        }
        public async Task<bool> GetPayState(long orderId)
        {
            string stateJson = await _payDal.GetPayState(orderId);
            PayResultEntity entity = System.Text.Json.JsonSerializer.Deserialize<PayResultEntity>(stateJson);
            if (entity == null)
                throw new Exception("获取订单状态失败");
            if (!entity.result)
                throw new Exception(entity.message);

            return entity.state == 1;
        }

        public async Task<string> GetPayUrl(long orderId, double payment)
        {
            PayEntity entity = new PayEntity
            {
                orderId = orderId,
                apiKey = "b043d31eff7df2233635654d7==",
                oneceId = Guid.NewGuid().ToString(),
                totalPay = payment
            };
            string urlJson = await _payDal.GetPayUrl(System.Text.Json.JsonSerializer.Serialize(entity));
            PayResultEntity result = System.Text.Json.JsonSerializer.Deserialize<PayResultEntity>(urlJson);
            if (result == null)
                throw new Exception("获取支付链接失败");
            if (!result.result)
                throw new Exception(result.message);

            return result.url;
        }
    }
}
