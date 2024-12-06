using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Modules.GrabLocate.Migrations
{
    /// <inheritdoc />
    public partial class i3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TargetCount",
                table: "ProductConfigs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetCount",
                table: "ProductConfigs");
        }
    }
}
