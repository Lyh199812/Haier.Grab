using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.IMU.DataHub.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "t_datahub_deviceinfo");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "t_datahub_deviceinfo");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConnectionEndTime",
                table: "t_datahub_deviceinfo",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ConnectionStartTime",
                table: "t_datahub_deviceinfo",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionEndTime",
                table: "t_datahub_deviceinfo");

            migrationBuilder.DropColumn(
                name: "ConnectionStartTime",
                table: "t_datahub_deviceinfo");

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "t_datahub_deviceinfo",
                type: "int",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "t_datahub_deviceinfo",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
