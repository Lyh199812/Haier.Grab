using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Modules.GrabLocate.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PLC_IPAddress",
                table: "CommonConfigs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PLC_IPAddress",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
