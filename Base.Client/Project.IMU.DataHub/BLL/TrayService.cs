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
    public class TrayService
    {
        private readonly TrayDAL trayDAL;

        public TrayService(TrayDAL _trayDAL)
        {
            trayDAL = _trayDAL;
        }

        /// <summary>
        /// 根据托盘ID查找托盘信息
        /// </summary>
        /// <param name="trayID">托盘ID</param>
        /// <returns>操作结果，包括托盘信息</returns>
        public OperateResult<TTray> Find(string trayID)
        {
            try
            {
                var tray = trayDAL.Query<TTray>(x => x.TrayID == trayID).FirstOrDefault();
                if (tray == null)
                    return new OperateResult<TTray> { IsSuccess = false, Message = "未找到对应的托盘ID", ErrorCode = 10012 };

                return new OperateResult<TTray> { IsSuccess = true, Message = "加载成功", Content = tray };
            }
            catch (Exception ex)
            {
                return new OperateResult<TTray> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10013 };
            }
        }

        /// <summary>
        /// 加载所有托盘信息
        /// </summary>
        /// <returns>操作结果，包括托盘信息列表</returns>
        public OperateResult<List<TTray>> LoadAll()
        {
            try
            {
                var trays = trayDAL.Query<TTray>(c => true).ToList();
                if (trays == null || trays.Count == 0)
                    return new OperateResult<List<TTray>> { IsSuccess = false, Message = "未找到任何托盘信息", ErrorCode = 10014, Content = null };

                return new OperateResult<List<TTray>> { IsSuccess = true, Message = "加载所有托盘信息成功", Content = trays };
            }
            catch (Exception ex)
            {
                return new OperateResult<List<TTray>> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10015, Content = null };
            }
        }




        public OperateResult<string> GetBatchID(string trayID)
        {
            try
            {
                // 查找托盘是否存在
                var tray = trayDAL.Query<TTray>(x => x.TrayID == trayID).FirstOrDefault();

                // 如果托盘不存在，则创建新记录
                if (tray == null)
                {
                    tray = new TTray
                    {
                        TrayID = trayID,
                        UseCount = 0, // 新建记录，使用次数为 1
                        CreationTime = DateTime.Now,
                        LastLoadTime = null,
                        Status = "进行中", // 默认状态可以为“新建”，可以根据需要修改
                    };

                    // 插入新记录
                    var result = trayDAL.Insert(tray);
                    if (!result.IsSuccess)
                    {
                        return new OperateResult<string> { IsSuccess = false, Message = "新建托盘记录失败", ErrorCode = 10020 };
                    }

                    return new OperateResult<string> { IsSuccess = true, Message = "托盘记录新建成功", Content = tray.CurrentBatchID };
                }

                // 如果托盘已存在，增加使用次数并生成新的批次号
                tray.UseCount += 1;
                if (tray.Status == "进行中")
                {
                    // 托盘状态为“进行中”，对批次号做处理
                    tray.CurrentBatchID = HandleBatchIDForInProgress(tray); // 调用处理方法进行批次号处理
                }
                else
                {
                    // 状态不是“进行中”，直接根据 TrayID 和 UseCount 生成批次号
                    tray.CurrentBatchID = $"{tray.TrayID}_{tray.UseCount}_{DateTime.Now:yyMMddHHmmss}";
                }

                // 更新记录
                var updateResult = trayDAL.Update(tray);
                if (!updateResult.IsSuccess)
                {
                    return new OperateResult<string> { IsSuccess = false, Message = "更新托盘记录失败", ErrorCode = 10021 };
                }

                return new OperateResult<string> { IsSuccess = true, Message = "批次号生成成功", Content = tray.CurrentBatchID };
            }
            catch (Exception ex)
            {
                return new OperateResult<string> { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10022 };
            }
        }

        /// <summary>
        /// 如果托盘状态是“进行中”，则对批次号进行特殊处理
        /// </summary>
        /// <param name="tray">托盘信息</param>
        /// <returns>处理后的批次号</returns>
        private string HandleBatchIDForInProgress(TTray tray)
        {
            // 这里你可以根据具体的规则来处理批次号
            // 例如：假设我们在批次号中增加一个特殊的标识符（比如使用一个前缀）
            // 你可以根据具体需求调整下面的代码
            string batchPrefix = "INPROGRESS"; // 假设“进行中”状态下的批次号需要加上前缀
            return $"{batchPrefix}-{tray.TrayID}-{tray.UseCount}";
        }
    }



}
