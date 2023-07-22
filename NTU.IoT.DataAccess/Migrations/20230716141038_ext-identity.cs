using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NTU.IoT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class extidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "boolean",
                nullable: true);

            
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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "device_types",
                columns: new[] { "Id", "name", "table_name", "topic_name" },
                values: new object[,]
                {
                    { new Guid("5e3ac143-4f24-49c0-af3e-60ca74d6ed7c"), "Environmental", "env", "Env" },
                    { new Guid("a13d5128-3cf5-4ce2-a881-eb703e1c4f44"), "Physio", "physio", "Physio" }
                });
        }
    }
}
