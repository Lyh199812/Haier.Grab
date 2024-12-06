using BXC.Client.MinotorModule.Models;
using DAL.EFCore;
using Device.DataConvertLib;
using Base.Client.Entity.BXC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Common;

namespace BXC.Client.DAL
{
    public class DBDAL
    {
        public DBDAL() 
        { 
        
        }
        public static OperateResult<List<BXCRecordEntity>> LoadBXCRecordEntitys()
        {
            OperateResult<List<BXCRecordEntity>> rst = new OperateResult<List<BXCRecordEntity>>();
            rst.IsSuccess = false;

            using (BXCDBConfig ctx = new BXCDBConfig())
            {
                try
                {
                    // 查询符合条件的记录
                    var query = ctx.records.AsQueryable();


                    List<BXCRecordEntity> records = query.ToList();

                    rst.IsSuccess = true;
                    rst.Content = records;
                }
                catch (Exception ex)
                {
                    rst.Message = $"查询数据库时发生错误: {ex.Message}";
                }
            }

            return rst;
        }
        public static OperateResult<List<BXCRecordEntity>> LoadBXCRecordEntitys(DateTime? startDateTime, DateTime? endDateTime)
        {
            OperateResult<List<BXCRecordEntity>> rst = new OperateResult<List<BXCRecordEntity>>();
            rst.IsSuccess = false;

            using (BXCDBConfig ctx = new BXCDBConfig())
            {
                try
                {
                    // 查询符合条件的记录
                    var query = ctx.records.AsQueryable();

                    if (startDateTime.HasValue)
                        query = query.Where(x => x.Time >= startDateTime.Value);

                    if (endDateTime.HasValue)
                        query = query.Where(x => x.Time <= endDateTime.Value);

    

                    List<BXCRecordEntity> records = query.ToList();

                    if (records.Count > 0)
                    {
                        rst.IsSuccess = true;
                        rst.Content = records;
                    }
                    else
                    {
                        rst.Message = "未找到符合条件的记录。";
                    }
                }
                catch (Exception ex)
                {
                    rst.Message = $"查询数据库时发生错误: {ex.Message}";
                }
            }

            return rst;
        }
        public static OperateResult ClearBXCRecordEntities()
        {
            OperateResult rst = new OperateResult();
            rst.IsSuccess = false;

            using (BXCDBConfig ctx = new BXCDBConfig())
            {
                try
                {
                    // 获取所有记录并删除
                    ctx.records.RemoveRange(ctx.records);
                    ctx.SaveChanges();

                    rst.IsSuccess = true;
                    rst.Message = "表格已成功清空。";
                }
                catch (Exception ex)
                {
                    rst.Message = $"清空表格时发生错误: {ex.Message}";
                }
            }

            return rst;
        }
        public static OperateResult<BXCConfig> LoadSelectedConfig()
        {
            OperateResult < BXCConfig > rst= new OperateResult<BXCConfig >();
            rst.IsSuccess = false;
            using (BXCDBConfig ctx=new BXCDBConfig())
            {
                BXCConfig bXCConfig = ctx.cfgs.Where(x => x.IsSelected == 1).FirstOrDefault();
               if(bXCConfig!=null)
                {
                    rst.IsSuccess = true;
                    rst.Content = bXCConfig;
                }
               else
                {
                    rst.IsSuccess = false;
                    rst.Message = $"加载数据库-无被选中配置文件";
                }
               return rst;
            }
        }


        public static OperateResult<BXCConfig> UpdateConfig(BXCConfig bXCConfig)
        {
            OperateResult<BXCConfig> rst = new OperateResult<BXCConfig>
            {
                IsSuccess = false
            };

            if (bXCConfig == null)
            {
                rst.Message = "参数不能为空";
                return rst;
            }

            try
            {
                using (BXCDBConfig ctx = new BXCDBConfig())
                {
                    ctx.cfgs.Update(bXCConfig);
                    ctx.SaveChanges();

                    rst.IsSuccess = true;
                    rst.Content = bXCConfig;
                }
            }
            catch (Exception ex)
            {
                rst.Message = $"数据库-配置文件保存失败: {ex.Message}";
            }

            return rst;
        }


        public static OperateResult<BXCRecordEntity> AddRecord(BXCRecordEntity bXCRecordEntity)
        {
            OperateResult<BXCRecordEntity> rst = new OperateResult<BXCRecordEntity>();
            rst.IsSuccess = false;

            using (BXCDBConfig ctx = new BXCDBConfig())
            {
                try
                {
                    ctx.records.Add(bXCRecordEntity);
                    ctx.SaveChanges(); // 保存更改到数据库

                    rst.IsSuccess = true;
                    rst.Content = bXCRecordEntity;
                }
                catch (Exception ex)
                {
                    rst.IsSuccess = false;
                    rst.Message = $"添加记录失败: {ex.Message}";
                }
            }

            return rst;
        }


        public static OperateResult<ProductionStatistics> AddOrLoadTodayRecord()
        {
            OperateResult<ProductionStatistics> rst = new OperateResult<ProductionStatistics>();
            rst.IsSuccess = false;
            DateTime today = DateTime.Today; // 获取今天的日期

            using (BXCDBConfig ctx = new BXCDBConfig())
            {
                try
                {
                    // 查找当天的 ProductionStatistics 记录
                    var existingRecord = ctx.ProductionStatistics
                        .FirstOrDefault(r => r.ProductionDate == today);

                    if (existingRecord != null)
                    {
                        // 如果存在，返回现有记录
                        rst.IsSuccess = true;
                        rst.Content = existingRecord;
                    }
                    else
                    {
                        // 如果不存在，创建新的记录
                        var newRecord = new ProductionStatistics
                        {
                            ProductionDate = today,
                            TotalProduced = 0,
                            QualifiedQuantity = 0,
                            UnqualifiedQuantity = 0,
                            Remarks = "",
                        };

                        ctx.ProductionStatistics.Add(newRecord);
                        ctx.SaveChanges(); // 保存新记录到数据库

                        rst.IsSuccess = true;
                        rst.Content = newRecord;
                    }
                }
                catch (Exception ex)
                {
                    rst.IsSuccess = false;
                    rst.Message = $"加载或添加记录失败: {ex.Message}";
                }
            }

            return rst;
        }


        public static OperateResult<ProductionStatistics> UpdateWithTestResult(DateTime productionDate, bool isQualified,bool isScanOK)
        {
            OperateResult<ProductionStatistics> rst = new OperateResult<ProductionStatistics>();
            rst.IsSuccess = false;

            using (BXCDBConfig ctx = new BXCDBConfig())
            {
                try
                {
                    // 查找指定日期的 ProductionStatistics 记录
                    var existingRecord = ctx.ProductionStatistics
                        .FirstOrDefault(r => r.ProductionDate == productionDate.Date);

                    if (existingRecord != null)
                    {
                        // 更新生产总数
                        existingRecord.TotalProduced += 1;

                        // 根据测试结果更新合格数量或不合格数量
                        if (isQualified)
                        {
                            existingRecord.QualifiedQuantity += 1;
                        }
                        else
                        {
                            if(!isScanOK)
                            {
                                existingRecord.NumScanFail += 1;
                            }
                            existingRecord.UnqualifiedQuantity += 1;
                        }

                        // 保存更改到数据库
                        ctx.SaveChanges();

                        rst.IsSuccess = true;
                        rst.Content = existingRecord;
                    }
                    else
                    {
                        rst.IsSuccess = false;
                        rst.Message = "未找到指定日期的记录";
                    }
                }
                catch (Exception ex)
                {
                    rst.IsSuccess = false;
                    rst.Message = $"更新记录失败: {ex.Message}";
                }
            }

            return rst;
        }


        public static OperateResult<ProductionStatistics> AddOrUpdateTodayRecordWithTestResult(bool isQualified,bool IsScanOK)
        {
            // 先获取今天的 ProductionStatistics 记录
            var result = AddOrLoadTodayRecord();

            if (!result.IsSuccess)
            {
                result.Message = "获取今天的记录失败";
                return result;
            }

            var todayRecord = result.Content;

            // 使用更新方法更新记录
            var updateResult = UpdateWithTestResult(todayRecord.ProductionDate, isQualified, IsScanOK);

            if (!updateResult.IsSuccess)
            {
                updateResult.Message = $"更新今天的记录失败: {updateResult.Message}";
                return updateResult;
            }

            return updateResult;
        }

        public static OperateResult<ProductionStatistics> ClearProductionStatistics()
        {
            OperateResult<ProductionStatistics> rst = new OperateResult<ProductionStatistics>();
            rst.IsSuccess = false;
            DateTime today = DateTime.Today;

            using (BXCDBConfig ctx = new BXCDBConfig())
            {
                try
                {
                    // 查找当天的 ProductionStatistics 记录
                    var existingRecord = ctx.ProductionStatistics
                        .FirstOrDefault(r => r.ProductionDate == today);

                    if (existingRecord != null)
                    {
                        // 如果存在，更新现有记录的时间为 DateTime.Now
                        existingRecord.ProductionDate = DateTime.Now;
                        ctx.SaveChanges();

                        // 创建新的今日记录
                        var newRecord = new ProductionStatistics
                        {
                            ProductionDate = today,
                            TotalProduced = 0,
                            QualifiedQuantity = 0,
                            UnqualifiedQuantity = 0,
                            Remarks = "",
                        };

                        ctx.ProductionStatistics.Add(newRecord);
                        ctx.SaveChanges();

                        rst.IsSuccess = true;
                        rst.Content = newRecord;
                    }
                    else
                    {
                        // 如果不存在今日记录，直接创建新的记录
                        var newRecord = new ProductionStatistics
                        {
                            ProductionDate = today,
                            TotalProduced = 0,
                            QualifiedQuantity = 0,
                            UnqualifiedQuantity = 0,
                            Remarks = "",
                        };

                        ctx.ProductionStatistics.Add(newRecord);
                        ctx.SaveChanges();

                        rst.IsSuccess = true;
                        rst.Content = newRecord;
                    }
                }
                catch (Exception ex)
                {
                    rst.IsSuccess = false;
                    rst.Message = $"加载或添加记录失败: {ex.Message}";
                }
            }

            return rst;
        }

    }
}
