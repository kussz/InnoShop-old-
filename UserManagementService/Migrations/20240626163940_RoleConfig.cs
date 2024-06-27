using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagementService.Migrations
{
    /// <inheritdoc />
    public partial class RoleConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("110a5e36-bb1b-400e-a31c-7bf6300a5f9d"), null, "Regular", "REGULAR" },
                    { new Guid("48dde680-94b9-4bd9-bf0c-f30822915815"), null, "Guest", "GUEST" },
                    { new Guid("ef10a471-8e5b-4b0d-a551-0690af4a3665"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("110a5e36-bb1b-400e-a31c-7bf6300a5f9d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("48dde680-94b9-4bd9-bf0c-f30822915815"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ef10a471-8e5b-4b0d-a551-0690af4a3665"));
        }
    }
}
