using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.IMU.DataHub.Migrations
{
    /// <inheritdoc />
    public partial class add_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "t_datahub_deviceinfo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "t_datahub_deviceinfo");
        }
    }
}
