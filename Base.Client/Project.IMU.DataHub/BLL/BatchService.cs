using Base.Client.Entity;
using Project.IMU.DataHub.DAL;
using Project.IMU.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.BLL
{
    public class BatchService
    {
        private readonly BatchDAL batchDAL;

        public BatchService(BatchDAL _batchDAL)
        {
            batchDAL = _batchDAL;
        }

        /// <summary>
        /// 根据批次ID查找批次信息
        /// </summary>
        /// <param name="batchID">批次ID</param>
        /// <returns>操作结果，包括批次信息</returns>
        public OperateResult<TBatch> Find(string batchID)
        {
            try
            {
                var batch = batchDAL.Query<TBatch>(x => x.BatchID == batchID).FirstOrDefault();
                if (batch == null)
                {
                    return new OperateResult<TBatch>
                    {
                        IsSuccess = false,
                        Message = "未找到对应的批次ID",
                        ErrorCode = 20001
                    };
                }

                return new OperateResult<TBatch>
                {
                    IsSuccess = true,
                    Message = "加载成功",
                    Content = batch
                };
            }
            catch (Exception ex)
            {
                return new OperateResult<TBatch>
                {
                    IsSuccess = false,
                    Message = ex.ToString(),
                    ErrorCode = 20002
                };
            }
        }

        /// <summary>
        /// 根据状态加载批次信息
        /// </summary>
        /// <param name="status">批次状态</param>
        /// <returns>操作结果，包括批次信息列表</returns>
        public OperateResult<List<TBatch>> LoadByStatus(string status)
        {
            try
            {
                // 根据状态过滤批次信息
                var batches = batchDAL.Query<TBatch>(c => c.Status == status).ToList();

                return new OperateResult<List<TBatch>>
                {
                    IsSuccess = true,
                    Message = $"加载状态为 '{status}' 的批次信息成功",
                    Content = batches
                };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<TBatch>>
                {
                    IsSuccess = false,
                    Message = ex.ToString(),
                    ErrorCode = 20006,
                    Content = null
                };
            }
        }


        /// <summary>
        /// 加载所有批次信息
        /// </summary>
        /// <returns>操作结果，包括批次信息列表</returns>
        public OperateResult<List<TBatch>> LoadAll()
        {
            try
            {
                var batches = batchDAL.Query<TBatch>(c => true).ToList();
                if (batches == null || batches.Count == 0)
                {
                    return new OperateResult<List<TBatch>>
                    {
                        IsSuccess = false,
                        Message = "未找到任何批次信息",
                        ErrorCode = 20003,
                        Content = null
                    };
                }

                return new OperateResult<List<TBatch>>
                {
                    IsSuccess = true,
                    Message = "加载所有批次信息成功",
                    Content = batches
                };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<TBatch>>
                {
                    IsSuccess = false,
                    Message = ex.ToString(),
                    ErrorCode = 20004,
                    Content = null
                };
            }
        }


        /// <summary>
        /// 更新批次状态
        /// </summary>
        /// <param name="batchID">批次ID</param>
        /// <param name="newStatus">新状态</param>
        /// <returns>更新操作结果</returns>
        public OperateResult UpdateStatus(string batchID, string newStatus)
        {
            try
            {
                var batch = batchDAL.Query<TBatch>(b => b.BatchID == batchID).FirstOrDefault();
                if (batch == null)
                {
                    return new OperateResult
                    {
                        IsSuccess = false,
                        Message = "未找到批次记录",
                        ErrorCode = 20005
                    };
                }

                batch.Status = newStatus;
                batch.LastUpdatedTime = DateTime.Now;

                var result = batchDAL.Update(batch);
                return result.IsSuccess
                    ? new OperateResult { IsSuccess = true, Message = "批次状态更新成功" }
                    : new OperateResult { IsSuccess = false, Message = "批次状态更新失败", ErrorCode = 20006 };
            }
            catch (Exception ex)
            {
                return new OperateResult
                {
                    IsSuccess = false,
                    Message = ex.ToString(),
                    ErrorCode = 20007
                };
            }
        }

        /// <summary>
        /// 根据站点查询批次信息
        /// </summary>
        /// <param name="station">站点名称</param>
        /// <returns>操作结果，包括批次信息列表</returns>
        public OperateResult<List<TBatch>> GetBatchesByStation(string station)
        {
            try
            {
                var batches = batchDAL.Query<TBatch>(b => b.CurrentStation == station).ToList();
                if (batches == null || batches.Count == 0)
                {
                    return new OperateResult<List<TBatch>>
                    {
                        IsSuccess = false,
                        Message = "未找到对应站点的批次信息",
                        ErrorCode = 20008
                    };
                }

                return new OperateResult<List<TBatch>>
                {
                    IsSuccess = true,
                    Message = "查询成功",
                    Content = batches
                };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<TBatch>>
                {
                    IsSuccess = false,
                    Message = ex.ToString(),
                    ErrorCode = 20009
                };
            }
        }

        /// <summary>
        /// 更新指定穴位的测试结果
        /// </summary>
        /// <param name="batchID">批次ID</param>
        /// <param name="positionIndex">穴位编号 (1-6)</param>
        /// <param name="barcode">条码</param>
        /// <param name="result">测试结果</param>
        /// <returns>更新操作结果</returns>
        public OperateResult UpdateTestResult(string batchID, int positionIndex, string barcode, string result)
        {
            try
            {
                var batch = batchDAL.Query<TBatch>(b => b.BatchID == batchID).FirstOrDefault();
                if (batch == null)
                {
                    return new OperateResult
                    {
                        IsSuccess = false,
                        Message = "未找到批次记录",
                        ErrorCode = 20010
                    };
                }

                switch (positionIndex)
                {
                    case 1:
                        batch.Position1Barcode = barcode;
                        batch.Position1Result = result;
                        break;
                    case 2:
                        batch.Position2Barcode = barcode;
                        batch.Position2Result = result;
                        break;
                    case 3:
                        batch.Position3Barcode = barcode;
                        batch.Position3Result = result;
                        break;
                    case 4:
                        batch.Position4Barcode = barcode;
                        batch.Position4Result = result;
                        break;
                    case 5:
                        batch.Position5Barcode = barcode;
                        batch.Position5Result = result;
                        break;
                    case 6:
                        batch.Position6Barcode = barcode;
                        batch.Position6Result = result;
                        break;
                    default:
                        return new OperateResult
                        {
                            IsSuccess = false,
                            Message = "无效的穴位编号",
                            ErrorCode = 20011
                        };
                }

                batch.LastUpdatedTime = DateTime.Now;
                var updateResult = batchDAL.Update(batch);

                return updateResult.IsSuccess
                    ? new OperateResult { IsSuccess = true, Message = "测试结果更新成功" }
                    : new OperateResult { IsSuccess = false, Message = "测试结果更新失败", ErrorCode = 20012 };
            }
            catch (Exception ex)
            {
                return new OperateResult
                {
                    IsSuccess = false,
                    Message = ex.ToString(),
                    ErrorCode = 20013
                };
            }
        }
    }

}
