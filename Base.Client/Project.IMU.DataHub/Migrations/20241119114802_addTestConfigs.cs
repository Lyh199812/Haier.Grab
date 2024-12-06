using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.IMU.DataHub.Migrations
{
    /// <inheritdoc />
    public partial class addTestConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "t_datahub_deviceinfo");

            migrationBuilder.DropColumn(
                name: "ProcessNo",
                table: "t_datahub_deviceinfo");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "t_datahub_deviceinfo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "StationName",
                table: "t_datahub_deviceinfo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "StationNo",
                table: "t_datahub_deviceinfo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "testConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PassFilePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FailFilePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ErrorFilePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testConfigs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "testConfigs");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "t_datahub_deviceinfo");

            migrationBuilder.DropColumn(
                name: "StationName",
                table: "t_datahub_deviceinfo");

            migrationBuilder.DropColumn(
                name: "StationNo",
                table: "t_datahub_deviceinfo");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "t_datahub_deviceinfo",
                type: "longtext",
                nullable: false,
                defaultValue: "0")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ProcessNo",
                table: "t_datahub_deviceinfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
