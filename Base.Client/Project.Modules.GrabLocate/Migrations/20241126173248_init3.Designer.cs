﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Modules.GrabLocate;

#nullable disable

namespace Project.Modules.GrabLocate.Migrations
{
    [DbContext(typeof(GrabDBConfig))]
    [Migration("20241126173248_init3")]
    partial class init3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Project.Modules.GrabLocate.Models.T_CommonConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("ImageBaseAngle")
                        .HasColumnType("float");

                    b.Property<float>("ImageBaseX")
                        .HasColumnType("float");

                    b.Property<float>("ImageBaseY")
                        .HasColumnType("float");

                    b.Property<float>("PLC_IPAddress")
                        .HasColumnType("float");

                    b.Property<float>("PLC_Port")
                        .HasColumnType("float");

                    b.Property<float>("RobotBaseAngle")
                        .HasColumnType("float");

                    b.Property<float>("RobotBaseX")
                        .HasColumnType("float");

                    b.Property<float>("RobotBaseY")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("CommonConfigs");
                });

            modelBuilder.Entity("Project.Modules.GrabLocate.Models.T_ProductConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit(1)");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit(1)");

                    b.Property<double>("MinScore")
                        .HasColumnType("double");

                    b.Property<string>("ModelPath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ProductConfigs");
                });
#pragma warning restore 612, 618
        }
    }
}