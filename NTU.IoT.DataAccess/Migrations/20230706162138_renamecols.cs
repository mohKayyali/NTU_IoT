using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTU.IoT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renamecols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "device_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "TopicName",
                table: "device_types",
                newName: "topic_name");

            migrationBuilder.RenameColumn(
                name: "TableName",
                table: "device_types",
                newName: "table_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "device_types",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "topic_name",
                table: "device_types",
                newName: "TopicName");

            migrationBuilder.RenameColumn(
                name: "table_name",
                table: "device_types",
                newName: "TableName");
        }
    }
}
