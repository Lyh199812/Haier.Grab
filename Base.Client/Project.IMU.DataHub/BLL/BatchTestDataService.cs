using Base.Client.Entity;
using Project.IMU.DataHub.DAL;
using Project.IMU.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.IMU.DataHub.BLL
{
    public class BatchTestDataService
    {
        private readonly BatchTestDataDAL TestDataDAL;

        public BatchTestDataService(BatchTestDataDAL _TestDataDAL)
        {
            TestDataDAL = _TestDataDAL;
        }

        /// <summary>
        /// 根据批次ID加载对应的测试数据
        /// </summary>
        /// <param name="batchID">批次ID</param>
        /// <returns>操作结果，包括测试数据列表</returns>
        public OperateResult<List<BatchTestData>> LoadByBatchID(string batchID)
        {
            try
            {
                var testData = TestDataDAL.Query<BatchTestData>(x => x.BatchID == batchID).ToList();
                if (testData == null || testData.Count == 0)
                    return new OperateResult<List<BatchTestData>> { IsSuccess = false, Message = "未找到对应批次的测试数据", ErrorCode = 20001 };

                return new OperateResult<List<BatchTestData>> { IsSuccess = true, Message = "加载测试数据成功", Content = testData };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<BatchTestData>> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 20002 };
            }
        }

        /// <summary>
        /// 保存或更新单条测试数据
        /// </summary>
        /// <param name="testData">测试数据实体</param>
        /// <returns>保存操作的结果</returns>
        public OperateResult Save(BatchTestData testData)
        {
            try
            {
                if (testData == null)
                    return new OperateResult { IsSuccess = false, Message = "测试数据不能为空", ErrorCode = 20003 };

                if (testData.ID == Guid.Empty) // 新建
                {
                    testData.ID = Guid.NewGuid();
                    var insertResult = TestDataDAL.Insert(testData);
                    if (insertResult.IsSuccess)
                        return new OperateResult { IsSuccess = true, Message = "测试数据保存成功", ErrorCode = 0 };
                    else
                        return new OperateResult { IsSuccess = false, Message = "测试数据保存失败", ErrorCode = 20004 };
                }
                else // 更新
                {
                    var updateResult = TestDataDAL.Update(testData);
                    if (updateResult.IsSuccess)
                        return new OperateResult { IsSuccess = true, Message = "测试数据更新成功", ErrorCode = 0 };
                    else
                        return new OperateResult { IsSuccess = false, Message = "测试数据更新失败", ErrorCode = 20005 };
                }
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 20006 };
            }
        }

        /// <summary>
        /// 保存或更新多条条测试数据
        /// </summary>
        /// <param name="testData">测试数据实体</param>
        /// <returns>保存操作的结果</returns>
        public OperateResult SaveAll(List<BatchTestData> testData)
        {
            try
            {
                if (testData == null)
                    return new OperateResult { IsSuccess = false, Message = "测试数据不能为空", ErrorCode = 20003 };

                foreach (var batch in testData)
                {
                    if (batch.ID == Guid.Empty) // 新建
                    {
                        batch.ID = Guid.NewGuid();
                    }
                }
                var updateResult = TestDataDAL.Insert(testData);
                if (updateResult.IsSuccess)
                    return new OperateResult { IsSuccess = true, Message = "测试数据更新成功", ErrorCode = 0 };
                else
                    return new OperateResult { IsSuccess = false, Message = "测试数据更新失败", ErrorCode = 20005 };
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 20006 };
            }
        }

        /// <summary>
        /// 加载某站点和批次的所有测试数据，按穴位排序
        /// </summary>
        /// <param name="batchID">批次ID</param>
        /// <param name="station">站点名称</param>
        /// <returns>操作结果，包括测试数据列表</returns>
        public OperateResult<List<BatchTestData>> LoadByBatchAndStation(string batchID, string station)
        {
            try
            {
                var testData = TestDataDAL.Query<BatchTestData>(x => x.BatchID == batchID && x.Station == station)
                                          .OrderBy(x => x.PositionIndex)
                                          .ToList();

                if (testData == null || testData.Count == 0)
                    return new OperateResult<List<BatchTestData>> { IsSuccess = false, Message = "未找到测试数据", ErrorCode = 20009 };

                return new OperateResult<List<BatchTestData>> { IsSuccess = true, Message = "加载测试数据成功", Content = testData };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<BatchTestData>> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 20010 };
            }
        }

        /// <summary>
        /// 加载所有测试数据
        /// </summary>
        /// <returns>操作结果，包括所有测试数据列表</returns>
        public OperateResult<List<BatchTestData>> LoadAll()
        {
            try
            {
                var testData = TestDataDAL.Query<BatchTestData>(x => true).OrderBy(x => x.BatchID).ToList();

                if (testData == null || testData.Count == 0)
                    return new OperateResult<List<BatchTestData>> { IsSuccess = false, Message = "未找到任何测试数据", ErrorCode = 20011 };

                return new OperateResult<List<BatchTestData>> { IsSuccess = true, Message = "加载所有测试数据成功", Content = testData };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<BatchTestData>> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 20012 };
            }
        }

    }




}
