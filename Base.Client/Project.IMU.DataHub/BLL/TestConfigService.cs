using Base.Client.Entity;
using Project.IMU.DataHub.DAL;
using Project.IMU.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.BLL
{
    public class TestConfigService
    {
        private readonly TestConfigDAL testConfigDAL;

        public TestConfigService(TestConfigDAL _testConfigDAL)
        {
            testConfigDAL = _testConfigDAL;
        }

        /// <summary>
        /// 加载单个配置项
        /// </summary>
        /// <param name="id">配置项的ID</param>
        /// <returns>操作结果，包括TTestConfig实体</returns>
        public OperateResult<TTestConfig> Load(int id)
        {
            try
            {
                var config = testConfigDAL.Find<TTestConfig>(id);
                if (config == null)
                    return new OperateResult<TTestConfig> { IsSuccess = false, Message = "未找到对应的配置项", ErrorCode = 10012 };

                return new OperateResult<TTestConfig> { IsSuccess = true, Message = "加载成功", Content = config };
            }
            catch (Exception ex)
            {
                return new OperateResult<TTestConfig> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10013 };
            }
        }

        /// <summary>
        /// 加载所有配置项
        /// </summary>
        /// <returns>操作结果，包括配置项列表</returns>
        public OperateResult<List<TTestConfig>> LoadAll()
        {
            try
            {
                var configs = testConfigDAL.Query<TTestConfig>(c => true).ToList();
                return new OperateResult<List<TTestConfig>> { IsSuccess = true, Message = "加载所有配置项成功",Content = configs };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<TTestConfig>> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10014, Content = null };
            }
        }

        /// <summary>
        /// 保存单个配置项
        /// </summary>
        /// <param name="config">要保存的配置项</param>
        /// <returns>保存操作的结果</returns>
        public OperateResult Save(TTestConfig config)
        {
            try
            {
                if (config == null)
                    return new OperateResult { IsSuccess = false, Message = "配置项不能为空", ErrorCode = 10015 };

                if (config.Id == 0)
                {
                    return testConfigDAL.Insert(config);
                }
                else
                {
                    return testConfigDAL.Update(config);
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
        public OperateResult Save(List<TTestConfig> configs)
        {
            try
            {
                if (configs == null || !configs.Any())
                    return new OperateResult { IsSuccess = false, Message = "配置项列表不能为空", ErrorCode = 10017 };

                var newConfigs = configs.Where(c => c.Id == 0).ToList();
                var existingConfigs = configs.Where(c => c.Id != 0).ToList();

                // 插入新数据
                var insertResult = testConfigDAL.Insert(newConfigs);
                if (!insertResult.IsSuccess)
                    return insertResult;

                // 更新已有数据
                var updateResult = testConfigDAL.Update(existingConfigs);
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
