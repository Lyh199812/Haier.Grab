using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.IMU.DataHub.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_testConfigs",
                table: "testConfigs");

            migrationBuilder.RenameTable(
                name: "testConfigs",
                newName: "t_datahub_testconfig");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_datahub_testconfig",
                table: "t_datahub_testconfig",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_t_datahub_testconfig",
                table: "t_datahub_testconfig");

            migrationBuilder.RenameTable(
                name: "t_datahub_testconfig",
                newName: "testConfigs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_testConfigs",
                table: "testConfigs",
                column: "Id");
        }
    }
}
