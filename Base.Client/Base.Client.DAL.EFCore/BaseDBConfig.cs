using Base.Client.EFCore.Configs;
using Base.Client.Entity.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Base.Client.EFCore
{
    public class BaseDBConfig: DbContext
    {
        // 通过构造函数注入 DbContextOptions
        public BaseDBConfig()
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1;Database=base_sys_db;User=root;Password=kc123456;Port=3306;Charset=utf8;TreatTinyAsBoolean=false;",
           new MySqlServerVersion(new Version(8, 0, 36)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 应用实体配置
            modelBuilder.ApplyConfiguration(new ProductionInfoCardModelConfiguration());
        }
        public DbSet<ProductionInfoCardModel> ProductionInfoCards { get; set; }
    }
}
