using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Modules.GrabLocate.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PLC_IPAddress",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "PLC_Port",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PLC_IPAddress",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "PLC_Port",
                table: "CommonConfigs");
        }
    }
}
