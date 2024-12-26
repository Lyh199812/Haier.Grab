using Base.Client.Entity;
using Project.Modules.GrabLocate;
using Project.Modules.GrabLocate.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Project.Modules.GrabLocate
{
    public class ProductConfigService
    {
        private readonly ProductConfigDAL _productConfigDAL;

        public ProductConfigService(ProductConfigDAL productConfigDAL)
        {
            _productConfigDAL = productConfigDAL;
        }

        public OperateResult<T_ProductConfig> GetCurrentConfig()
        {
            try
            {
                // 使用 AsNoTracking 来避免跟踪
                var productConfig = _productConfigDAL.QueryAsTracking<T_ProductConfig>(x => x.IsSelected == true).AsNoTracking().FirstOrDefault();
                if (productConfig == null)
                {
                    return new OperateResult<T_ProductConfig> { IsSuccess = false, Message = "未找到对应的产品配置", ErrorCode = 10012 };
                }
                return new OperateResult<T_ProductConfig> { IsSuccess = true, Message = "加载成功", Content = productConfig };
            }
            catch (Exception ex)
            {
                return new OperateResult<T_ProductConfig> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10013 };
            }
        }


        /// <summary>
        /// 保存单个配置项
        /// </summary>
        /// <param name="config">要保存的配置项</param>
        /// <returns>保存操作的结果</returns>
        public OperateResult Save(T_ProductConfig config)
        {
            try
            {
                if (config == null)
                    return new OperateResult { IsSuccess = false, Message = "配置项不能为空", ErrorCode = 10015 };

                if (config.Id == 0)
                {
                    return _productConfigDAL.Insert(config);
                }
                else
                {
                    return _productConfigDAL.Update(config);
                }
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10016 };
            }
        }

        /// <summary>
        /// 根据产品配置 ID 查找产品配置
        /// </summary>
        public OperateResult<T_ProductConfig> FindProductConfig(int id)
        {
            try
            {
                var productConfig = _productConfigDAL.Find<T_ProductConfig>(id);
                if (productConfig == null)
                {
                    return new OperateResult<T_ProductConfig> { IsSuccess = false, Message = "未找到对应的产品配置", ErrorCode = 10012 };
                }
                return new OperateResult<T_ProductConfig> { IsSuccess = true, Message = "加载成功", Content = productConfig };
            }
            catch (Exception ex)
            {
                return new OperateResult<T_ProductConfig> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10013 };
            }
        }


        /// <summary>
        /// 加载所有产品配置
        /// </summary>
        public OperateResult<List<T_ProductConfig>> GetAllProductConfigs()
        {
            try
            {
                _productConfigDAL.dbContext.ChangeTracker.Clear();
                var productConfigs = _productConfigDAL.Query<T_ProductConfig>(c => true).OrderByDescending(x => x.IsSelected).ThenBy(x => x.Id).ToList();
                if (productConfigs == null || productConfigs.Count == 0)
                {
                    return new OperateResult<List<T_ProductConfig>> { IsSuccess = false, Message = "未找到任何产品配置", ErrorCode = 10014, Content = null };
                }
                return new OperateResult<List<T_ProductConfig>> { IsSuccess = true, Message = "加载所有产品配置成功", Content = productConfigs };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<T_ProductConfig>> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10015, Content = null };
            }
        }

        /// <summary>
        /// 新建产品配置
        /// </summary>
        public OperateResult AddProductConfig(T_ProductConfig productConfig)
        {
            try
            {
                var result = _productConfigDAL.Insert(productConfig);
                if (!result.IsSuccess)
                {
                    return new OperateResult { IsSuccess = false, Message = "新建产品配置失败", ErrorCode = 10016 };
                }
                return new OperateResult { IsSuccess = true, Message = "新建产品配置成功" };
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10017 };
            }
        }

        /// <summary>
        /// 更新产品配置
        /// </summary>
        public OperateResult UpdateProductConfig(T_ProductConfig productConfig)
        {
            try
            {
                var result = _productConfigDAL.Update(productConfig);
                if (!result.IsSuccess)
                {
                    return new OperateResult { IsSuccess = false, Message = "更新产品配置失败", ErrorCode = 10018 };
                }
                return new OperateResult { IsSuccess = true, Message = "更新产品配置成功" };
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10019 };
            }
        }

        /// <summary>
        /// 删除产品配置
        /// </summary>
        public OperateResult DeleteProductConfig(int id)
        {
            try
            {
                var productConfig = _productConfigDAL.QueryAsTracking<T_ProductConfig>(x => x.Id == id).FirstOrDefault();
                if (productConfig == null)
                {
                    return new OperateResult { IsSuccess = false, Message = "未找到对应的产品配置", ErrorCode = 10020 };
                }
                var result = _productConfigDAL.Delete(productConfig);
                if (!result.IsSuccess)
                {
                    return new OperateResult { IsSuccess = false, Message = "删除产品配置失败", ErrorCode = 10021 };
                }
                
                return new OperateResult { IsSuccess = true, Message = "删除产品配置成功" };
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10022 };
            }
        }
    }
}
