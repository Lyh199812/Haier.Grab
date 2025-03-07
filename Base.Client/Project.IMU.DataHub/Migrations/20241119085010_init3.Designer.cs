﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.IMU.DataHub.DAL;

#nullable disable

namespace Project.IMU.DataHub.Migrations
{
    [DbContext(typeof(IMUDataHubDBConfig))]
    [Migration("20241119085010_init3")]
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

            modelBuilder.Entity("Project.IMU.DataHub.Models.TDeviceInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ConnectionEndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ConnectionStartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("longtext")
                        .HasDefaultValue("0");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("longtext")
                        .HasDefaultValue("0");

                    b.Property<int>("ProcessNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("t_datahub_deviceinfo", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
