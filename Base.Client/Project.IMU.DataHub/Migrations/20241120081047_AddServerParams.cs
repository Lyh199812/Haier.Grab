using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.IMU.DataHub.Migrations
{
    /// <inheritdoc />
    public partial class AddServerParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsServerEditable",
                table: "t_datahub_testconfig",
                type: "bit(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ServerIPAddress",
                table: "t_datahub_testconfig",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ServerPort",
                table: "t_datahub_testconfig",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsServerEditable",
                table: "t_datahub_testconfig");

            migrationBuilder.DropColumn(
                name: "ServerIPAddress",
                table: "t_datahub_testconfig");

            migrationBuilder.DropColumn(
                name: "ServerPort",
                table: "t_datahub_testconfig");
        }
    }
}
