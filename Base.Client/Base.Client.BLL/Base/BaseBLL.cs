using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IDAL;
using static Unity.Storage.RegistrationSet;

namespace Base.Client.BLL
{
    public class BaseBLL : IBaseBLL
    {
        IBaseDal _baseDal;
        public BaseBLL(IBaseDal baseDal)
        {
            _baseDal = baseDal;
        }
        public async Task<List<ColorEntity>> GetAutoColors()
        {
            var resutlJson = await _baseDal.GetAutoColors();
            ResultEntity<List<ColorEntity>> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<ColorEntity>>>(resutlJson);

            if (result.state == 200)
                return result.data;
            else
                throw new Exception(result.exceptionMessage);
        }

        public async Task<List<FeeModeEntity>> GetFeeModes()
        {
            var resutlJson = await _baseDal.GetFeeModes();
            ResultEntity<List<FeeModeEntity>> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<FeeModeEntity>>>(resutlJson);

            if (result.state == 200)
                return result.data;
            else
                throw new Exception(result.exceptionMessage);
        }

        public async Task<List<ColorEntity>> GetLicenseColors()
        {
            var resutlJson = await _baseDal.GetLicenseColors();
            ResultEntity<List<ColorEntity>> result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<ColorEntity>>>(resutlJson);

            if (result.state == 200)
                return result.data;
            else
                throw new Exception(result.exceptionMessage);
        }

        public async Task<bool> SaveAutoColor(ColorEntity colorEntity)
        {
            string json = await _baseDal.SaveAutoColor(System.Text.Json.JsonSerializer.Serialize(colorEntity));
            return this.CheckBoolState(json);
        }

        public async Task<bool> SaveFeeMode(FeeModeEntity feeModeEntity)
        {
            string json = await _baseDal.SaveAutoColor(System.Text.Json.JsonSerializer.Serialize(feeModeEntity));
            return this.CheckBoolState(json);
        }

        public async Task<bool> SaveLicenseColor(ColorEntity colorEntity)
        {
            string json = await _baseDal.SaveLicenseColor(System.Text.Json.JsonSerializer.Serialize(colorEntity));
            return this.CheckBoolState(json);
        }

        private bool CheckBoolState(string json)
        {
            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<bool>>(json);
            if (result != null)
            {
                if (result.state == 200)
                    return result.data;
                else
                    throw new Exception(result.exceptionMessage);
            }
            else
                throw new Exception("保存数据异常");
        }
    }
}
