using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.IMU.DataHub.Migrations
{
    /// <inheritdoc />
    public partial class reNameTrayTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_trayTable",
                table: "trayTable");

            migrationBuilder.RenameTable(
                name: "trayTable",
                newName: "t_datahub_tray");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_datahub_tray",
                table: "t_datahub_tray",
                column: "TrayID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_t_datahub_tray",
                table: "t_datahub_tray");

            migrationBuilder.RenameTable(
                name: "t_datahub_tray",
                newName: "trayTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trayTable",
                table: "trayTable",
                column: "TrayID");
        }
    }
}
