using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SA_Online_Mart.Migrations
{
    /// <inheritdoc />
    public partial class GUIDUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3f47a63-92c4-432f-8146-6cdb54e4f4e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9a1b6c4-7f6b-49f0-a5e5-ceddb46e5c2b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1f2b3c4-d5e6-47f8-9a0b-c1d2e3f4a5b6", null, "admin", "ADMIN" },
                    { "b6a5f4e3-d2c1-0b9a-8f7e-6d5c4b3f2a1f", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1f2b3c4-d5e6-47f8-9a0b-c1d2e3f4a5b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6a5f4e3-d2c1-0b9a-8f7e-6d5c4b3f2a1f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c3f47a63-92c4-432f-8146-6cdb54e4f4e2", null, "customer", "CUSTOMER" },
                    { "e9a1b6c4-7f6b-49f0-a5e5-ceddb46e5c2b", null, "admin", "ADMIN" }
                });
        }
    }
}
