using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Modules.GrabLocate.Migrations
{
    /// <inheritdoc />
    public partial class i6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "AutoSwitchRecipeEnabled",
                table: "CommonConfigs",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AutoSwitchRecipeEnabled",
                table: "CommonConfigs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit(1)")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
