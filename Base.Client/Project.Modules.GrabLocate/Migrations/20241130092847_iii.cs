using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Modules.GrabLocate.Migrations
{
    /// <inheritdoc />
    public partial class iii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GrbWidth",
                table: "ProductConfigs",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrbWidth",
                table: "ProductConfigs");
        }
    }
}
