using System;
using System.Threading.Tasks;
using Base.Client.IBLL;
using Base.Client.IDAL;
using System.Text.Json;
using Base.Client.Entity;

namespace Base.Client.BLL
{
    public class LoginBLL : ILoginBLL
    {
        ILoginDal _loginDal;
        public LoginBLL(ILoginDal loginDal)
        {
            _loginDal = loginDal;
        }
        public async Task<UserEntity> Login(string un, string pwd)
        {
            // 将DAL返回的Json字符串    反序列化成   直接对象
            string result = await _loginDal.Login(un, pwd);

            //ResultEntity<List<string>>
            ResultEntity<UserEntity> re = JsonSerializer.Deserialize<ResultEntity<UserEntity>>(result);
            if (re != null)
            {
                if (re.state == 200)
                {
                    return re.data;
                }
                else
                {
                    // 记录日志   异常码   500     501   其他错
                    throw new Exception(re.exceptionMessage);
                }
            }
            return null;
        }
    }
}
