using Base.Client.Entity.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.EFCore.Configs
{
    public class ProductionInfoCardModelConfiguration : IEntityTypeConfiguration<ProductionInfoCardModel>
    {
        public void Configure(EntityTypeBuilder<ProductionInfoCardModel> builder)
        {
            // 配置表名
            builder.ToTable("ProductionInfoCards");

            // 配置主键
            builder.HasKey(p => p.Id);

            // 配置列属性
            builder.Property(p => p.ProductionCount)
                   .IsRequired() // 配置为必填字段
                   .HasDefaultValue(0); // 设置默认值

            builder.Property(p => p.DefectiveCount)
                   .IsRequired() // 配置为必填字段
                   .HasDefaultValue(0); // 设置默认值

            builder.Property(p => p.PassRate)
                   .IsRequired() // 配置为必填字段
                   .HasPrecision(5, 2); // 设置合格率的精度

            builder.Property(p => p.UpdateTime)
                   .IsRequired() // 配置为必填字段
                   .HasColumnType("datetime"); // 设置数据库列类型为 datetime

            builder.Property(p => p.StartTime)
                   .IsRequired() // 配置为必填字段
                   .HasColumnType("datetime"); // 设置数据库列类型为 datetime
        }
    }
}
