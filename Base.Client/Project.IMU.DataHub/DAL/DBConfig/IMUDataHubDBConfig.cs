using Base.Client.Entity.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.IMU.DataHub.DAL.Config;
using Project.IMU.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.IMU.DataHub.DAL
{
    public class IMUDataHubDBConfig : DbContext
    {
        // 通过构造函数注入 DbContextOptions
        public IMUDataHubDBConfig()
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1;Database=imu_datahub_db;User=root;Password=kc123456;Port=3306;Charset=utf8;TreatTinyAsBoolean=false;",
           new MySqlServerVersion(new Version(8, 0, 36)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 应用实体配置
            modelBuilder.ApplyConfiguration(new TDeviceInfoConfig());
        }

        public DbSet<TTestConfig> testConfigs { get; set; }
        public DbSet<TTray> trayTable { get; set; }
        public DbSet<TBatch> batchs { get; set; }
        public DbSet<BatchTestData> testDatas { get; set; }
        
    }
}
