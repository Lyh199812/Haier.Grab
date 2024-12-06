using Base.Client.Entity;
using Project.Modules.GrabLocate;
using Project.Modules.GrabLocate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Modules.GrabLocate
{
    public class CommonConfigService
    {
        private readonly CommonConfigDAL DAL;

        public CommonConfigService(CommonConfigDAL _DAL)
        {
            DAL = _DAL;
        }

        /// <summary>
        /// 加载单个配置项
        /// </summary>
        /// <param name="id">配置项的ID</param>
        /// <returns>操作结果，包括T_CommonConfig实体</returns>
        public OperateResult<T_CommonConfig> Load(int id)
        {
            try
            {
                var config = DAL.Find<T_CommonConfig>(id);
                if (config == null)
                    return new OperateResult<T_CommonConfig> { IsSuccess = false, Message = "未找到对应的配置项", ErrorCode = 10012 };

                return new OperateResult<T_CommonConfig> { IsSuccess = true, Message = "加载成功", Content = config };
            }
            catch (Exception ex)
            {
                return new OperateResult<T_CommonConfig> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10013 };
            }
        }

        /// <summary>
        /// 加载所有配置项
        /// </summary>
        /// <returns>操作结果，包括配置项列表</returns>
        public OperateResult<List<T_CommonConfig>> LoadAll()
        {
            try
            {
                var configs = DAL.Query<T_CommonConfig>(c => true).ToList();
                return new OperateResult<List<T_CommonConfig>> { IsSuccess = true, Message = "加载所有配置项成功", Content = configs };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<T_CommonConfig>> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10014, Content = null };
            }
        }

        /// <summary>
        /// 保存单个配置项
        /// </summary>
        /// <param name="config">要保存的配置项</param>
        /// <returns>保存操作的结果</returns>
        public OperateResult Save(T_CommonConfig config)
        {
            try
            {
                if (config == null)
                    return new OperateResult { IsSuccess = false, Message = "配置项不能为空", ErrorCode = 10015 };

                if (config.Id == 0)
                {
                    return DAL.Insert(config);
                }
                else
                {
                    return DAL.Update(config);
                }
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10016 };
            }
        }

        /// <summary>
        /// 批量保存配置项
        /// </summary>
        /// <param name="configs">要保存的配置项列表</param>
        /// <returns>保存操作的结果</returns>
        public OperateResult Save(List<T_CommonConfig> configs)
        {
            try
            {
                if (configs == null || !configs.Any())
                    return new OperateResult { IsSuccess = false, Message = "配置项列表不能为空", ErrorCode = 10017 };

                var newConfigs = configs.Where(c => c.Id == 0).ToList();
                var existingConfigs = configs.Where(c => c.Id != 0).ToList();

                // 插入新数据
                var insertResult = DAL.Insert(newConfigs);
                if (!insertResult.IsSuccess)
                    return insertResult;

                // 更新已有数据
                var updateResult = DAL.Update(existingConfigs);
                if (!updateResult.IsSuccess)
                    return updateResult;

                return new OperateResult { IsSuccess = true, Message = "批量保存成功" };
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10018 };
            }
        }
    }

}
