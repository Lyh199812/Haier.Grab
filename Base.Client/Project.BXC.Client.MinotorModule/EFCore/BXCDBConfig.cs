using Base.Client.Entity.BXC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFCore
{

    public class BXCDBConfig : DbContext
    {
        public DbSet<BXCRecordEntity> records { get; set; }
        public DbSet<BXCConfig> cfgs { get; set; }
        public DbSet<ProductionStatistics> ProductionStatistics {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // 使用 UseMySql 方法配置连接字符串，并指定 MySQL 服务器版本
            var connectionString = "Database=bxcDB;Data Source=127.0.0.1;Port=3306;User Id=root;Password=kc123456;Charset=utf8;TreatTinyAsBoolean=false;";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21)); // 根据你使用的 MySQL 版本修改

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 从当前程序集加载所有的 IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }

}
