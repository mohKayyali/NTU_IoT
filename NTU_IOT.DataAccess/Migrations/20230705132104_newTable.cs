using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTU_IOT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_deviceTypes",
                table: "deviceTypes");

            migrationBuilder.RenameTable(
                name: "deviceTypes",
                newName: "device_types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_device_types",
                table: "device_types",
                column: "Id");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_device_types",
                table: "device_types");

            migrationBuilder.RenameTable(
                name: "device_types",
                newName: "deviceTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deviceTypes",
                table: "deviceTypes",
                column: "Id");
        }
    }
}
