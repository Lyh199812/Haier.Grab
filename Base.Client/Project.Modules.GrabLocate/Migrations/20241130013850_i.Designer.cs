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
    [Migration("20241130013850_i")]
    partial class i
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

                    b.Property<bool>("AutoSwitchRecipeEnabled")
                        .HasColumnType("bit(1)");

                    b.Property<float>("ImageBaseAngle")
                        .HasColumnType("float");

                    b.Property<float>("ImageBaseX")
                        .HasColumnType("float");

                    b.Property<float>("ImageBaseY")
                        .HasColumnType("float");

                    b.Property<float>("ROI_MaxArea")
                        .HasColumnType("float");

                    b.Property<int>("ROI_MaxGray")
                        .HasColumnType("int");

                    b.Property<float>("ROI_MinArea")
                        .HasColumnType("float");

                    b.Property<int>("ROI_MinGray")
                        .HasColumnType("int");

                    b.Property<float>("ROI_X1")
                        .HasColumnType("float");

                    b.Property<float>("ROI_X2")
                        .HasColumnType("float");

                    b.Property<float>("ROI_Y1")
                        .HasColumnType("float");

                    b.Property<float>("ROI_Y2")
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

                    b.Property<int>("TargetCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductConfigs");
                });
#pragma warning restore 612, 618
        }
    }
}