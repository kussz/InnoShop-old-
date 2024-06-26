using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "ManipulationAccess", "RoleName", "PostAccess" },
                values: new object[,]
                {
                    { new Guid("2dd259f0-e447-4bf5-82a1-4646f89712e5"), false, "Regular", true },
                    { new Guid("78dbc134-6233-4605-962a-17263cec1c95"), false, "Guest", false },
                    { new Guid("bba84abf-a571-463f-b4e0-fcec5729c454"), true, "Admin", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: new Guid("2dd259f0-e447-4bf5-82a1-4646f89712e5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: new Guid("78dbc134-6233-4605-962a-17263cec1c95"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: new Guid("bba84abf-a571-463f-b4e0-fcec5729c454"));
        }
    }
}
