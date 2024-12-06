using Base.Client.Entity.BXC;
using BXC.Client.MinotorModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFCore.Config
{
    public class BXCRecordEntityConfig : IEntityTypeConfiguration<BXCRecordEntity>
    {
        public void Configure(EntityTypeBuilder<BXCRecordEntity> builder)
        {
            // 配置表名
            builder.ToTable("BXCRecord");

            // 配置主键
            builder.HasKey(e => e.Id);

            // 配置自增主键
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }

       
    }
}
