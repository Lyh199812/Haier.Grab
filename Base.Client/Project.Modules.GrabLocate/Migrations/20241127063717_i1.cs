using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Modules.GrabLocate.Migrations
{
    /// <inheritdoc />
    public partial class i1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PLC_IPAddress",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "PLC_Port",
                table: "CommonConfigs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PLC_IPAddress",
                table: "CommonConfigs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "PLC_Port",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
