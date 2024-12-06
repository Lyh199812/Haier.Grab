using Base.Client.Entity;
using Base.Client.Entity.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IBLL.Controls.ProductionInfoCard
{
    public interface IProductionInfoCardBLL
    {
        // 新增生产记录
        OperateResult AddProductionInfoCard(ProductionInfoCardModel card);

        // 新增合格记录
        OperateResult AddQualified(int productionCount);

        // 新增不合格记录
        OperateResult AddDefective(int defectiveCount);

        // 查询所有记录
        List<ProductionInfoCardModel> GetAllProductionInfoCards();

        // 查询某个记录
        ProductionInfoCardModel GetProductionInfoCardById(int id);

        // 查询历史记录（根据起始时间或其他条件）
        List<ProductionInfoCardModel> GetHistoryProductionInfoCards(DateTime startTime, DateTime endTime);

        // 更新生产记录
        OperateResult UpdateProductionInfoCard(ProductionInfoCardModel card);

        // 删除生产记录
        OperateResult DeleteProductionInfoCard(int id);

        // 重新计算并新建一条数据
        OperateResult RecalculateAndCreateNewRecord();

        // 加载当前记录（按规则：开始时间最新的记录）
        OperateResult LoadLatestRecord();
    }

}
