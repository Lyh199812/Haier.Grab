using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Modules.GrabLocate.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ROI_MaxArea",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ROI_MaxGray",
                table: "CommonConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ROI_MinArea",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ROI_MinGray",
                table: "CommonConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ROI_X1",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ROI_X2",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ROI_Y1",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ROI_Y2",
                table: "CommonConfigs",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ROI_MaxArea",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "ROI_MaxGray",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "ROI_MinArea",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "ROI_MinGray",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "ROI_X1",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "ROI_X2",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "ROI_Y1",
                table: "CommonConfigs");

            migrationBuilder.DropColumn(
                name: "ROI_Y2",
                table: "CommonConfigs");
        }
    }
}
