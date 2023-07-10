using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NTU.IoT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedDevicetypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "device_types",
                columns: new[] { "Id", "name", "table_name", "topic_name" },
                values: new object[,]
                {
                    { new Guid("35cadad3-d405-43a2-a706-389bb94cd6ad"), "Environmental", "env", "Env" },
                    { new Guid("5c04a400-eaab-4530-ade5-7a8dd9527d24"), "Physio", "physio", "Physio" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "device_types",
                keyColumn: "Id",
                keyValue: new Guid("35cadad3-d405-43a2-a706-389bb94cd6ad"));

            migrationBuilder.DeleteData(
                table: "device_types",
                keyColumn: "Id",
                keyValue: new Guid("5c04a400-eaab-4530-ade5-7a8dd9527d24"));
        }
    }
}
