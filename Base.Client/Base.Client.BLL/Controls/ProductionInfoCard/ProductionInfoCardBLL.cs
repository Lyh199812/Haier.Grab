using Base.Client.Entity;
using Base.Client.Entity.Message;
using Base.Client.IBLL.Controls.ProductionInfoCard;
using Base.Client.IDAL.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Base.Client.BLL.Controls
{
    public class ProductionInfoCardBLL : BindableBase, IProductionInfoCardBLL
    {
        private IProductionInfoCardDAL infoDAL;
        private ProductionInfoCardModel _currentProductionInfo;
        public ProductionInfoCardModel currentProductionInfo
        {
            get { return _currentProductionInfo; }
            set { _currentProductionInfo = value;
                RaisePropertyChanged(); 
            }
        }


        public ProductionInfoCardBLL(IProductionInfoCardDAL productionInfoCardDAL)
        {
            infoDAL = productionInfoCardDAL;
        }
        //清空
        public DelegateCommand ClearCommand {  get; set; }
        // 新增生产记录
        public OperateResult AddProductionInfoCard(ProductionInfoCardModel card)
        {
            return infoDAL.Insert(card);
        }

        // 新增合格记录
        public OperateResult AddQualified(int productionCount)
        {
            currentProductionInfo.ProductionCount += productionCount;  // 增加生产数量
            currentProductionInfo.DefectiveCount = 0;  // 不合格数量清零
            currentProductionInfo.PassRate = (double)currentProductionInfo.ProductionCount /
                                             (currentProductionInfo.ProductionCount + currentProductionInfo.DefectiveCount) * 100;

            currentProductionInfo.UpdateTime = DateTime.Now;  // 更新时间
            return UpdateProductionInfoCard(currentProductionInfo);
        }

        // 新增不合格记录
        public OperateResult AddDefective(int defectiveCount)
        {
            currentProductionInfo.DefectiveCount += defectiveCount;  // 增加不合格数量
            currentProductionInfo.PassRate = (double)currentProductionInfo.ProductionCount /
                                             (currentProductionInfo.ProductionCount + currentProductionInfo.DefectiveCount) * 100;

            currentProductionInfo.UpdateTime = DateTime.Now;  // 更新时间
            return UpdateProductionInfoCard(currentProductionInfo);
        }

        // 查询所有记录
        public List<ProductionInfoCardModel> GetAllProductionInfoCards()
        {
            return infoDAL.Query<ProductionInfoCardModel>(x => true).ToList();
        }

        // 查询某个记录
        public ProductionInfoCardModel GetProductionInfoCardById(int id)
        {
            return infoDAL.Query<ProductionInfoCardModel>(x => x.Id == id).FirstOrDefault();
        }

        // 查询历史记录（根据起始时间或其他条件）
        public List<ProductionInfoCardModel> GetHistoryProductionInfoCards(DateTime startTime, DateTime endTime)
        {
            return infoDAL.Query<ProductionInfoCardModel>(x => x.StartTime >= startTime && x.StartTime <= endTime)
                          .OrderBy(x => x.StartTime)  // 排序规则可以根据需求更改
                          .ToList();
        }

        // 更新生产记录
        public OperateResult UpdateProductionInfoCard(ProductionInfoCardModel card)
        {
            return infoDAL.Update<ProductionInfoCardModel>(card);
        }

        // 删除生产记录
        public OperateResult DeleteProductionInfoCard(int id)
        {
            return infoDAL.Delete<ProductionInfoCardModel>(id);
        }

        // 重新计算并新建一条数据
        public OperateResult RecalculateAndCreateNewRecord()
        {
            // 清除当前记录（可以根据业务需求选择是否删除当前记录）
            currentProductionInfo = new ProductionInfoCardModel
            {
                StartTime = DateTime.Now,  // 设置当前时间为开始时间
                UpdateTime = DateTime.Now,  // 更新时间为当前时间
                ProductionCount = 0,  // 重置生产数量
                DefectiveCount = 0,  // 重置不合格数量
                PassRate = 100  // 重置合格率
            };

            // 新建并保存一条记录
            return AddProductionInfoCard(currentProductionInfo);
            
        }

        // 加载规则：搜索开始时间最新的记录，不存在则新建
        public OperateResult LoadLatestRecord()
        {
            // 获取开始时间最新的记录
            currentProductionInfo = infoDAL.Query<ProductionInfoCardModel>(x => true)
                                             .OrderByDescending(x => x.Id)
                                             .FirstOrDefault();

            // 如果没有记录，创建一个新的记录并赋值给当前记录
            if (currentProductionInfo == null)
            {
                currentProductionInfo = new ProductionInfoCardModel
                {
                    StartTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                return AddProductionInfoCard(currentProductionInfo);  // 新建记录并保存
            }
            else
            {
                return OperateResult.CreateSuccessResult();
            }
        }
    }

}
