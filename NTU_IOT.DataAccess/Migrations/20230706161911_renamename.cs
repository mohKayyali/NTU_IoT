using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTU_IOT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renamename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceTypeName",
                table: "device_types",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "device_types",
                newName: "DeviceTypeName");
        }
    }
}
