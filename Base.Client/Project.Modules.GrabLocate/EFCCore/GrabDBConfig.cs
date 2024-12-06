using Base.Client.Entity.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Modules.GrabLocate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Modules.GrabLocate
{
    //Add-Migration -Project Project.Modules.GrabLocate init1
    public class GrabDBConfig : DbContext
    {
        // 通过构造函数注入 DbContextOptions
        public GrabDBConfig()
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1;Database=haier_grab_db;User=root;Password=kc123456;Port=3306;Charset=utf8;TreatTinyAsBoolean=false;",
           new MySqlServerVersion(new Version(8, 0, 36)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // DbSet 属性，表示与数据库表的交互
        public DbSet<T_CommonConfig> CommonConfigs { get; set; }
        public DbSet<T_ProductConfig> ProductConfigs { get; set; }
    }
}
