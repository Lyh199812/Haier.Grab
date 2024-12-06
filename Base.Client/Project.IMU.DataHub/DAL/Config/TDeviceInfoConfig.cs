using Base.Client.Entity.Message;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.IMU.DataHub.Models;

namespace Project.IMU.DataHub.DAL.Config
{
    public class TDeviceInfoConfig : IEntityTypeConfiguration<TDeviceInfo>
    {
        public void Configure(EntityTypeBuilder<TDeviceInfo> builder)
        {
            // 配置表名
            builder.ToTable("t_datahub_deviceinfo");

            // 配置主键
            builder.HasKey(p => p.Id);

            // 配置列属性
            builder.Property(p => p.DeviceName)
                   .IsRequired() // 配置为必填字段
                   .HasDefaultValue(0); // 设置默认值

  


        }
    }
}
