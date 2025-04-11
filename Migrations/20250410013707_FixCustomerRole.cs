using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SA_Online_Mart.Migrations
{
    /// <inheritdoc />
    public partial class FixCustomerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12d1176f-6158-42b5-9844-2bed5c6722b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af99930b-cabf-4a71-a04f-bd722e5206af");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c3f47a63-92c4-432f-8146-6cdb54e4f4e2", null, "staff", "STAFF" },
                    { "e9a1b6c4-7f6b-49f0-a5e5-ceddb46e5c2b", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "12d1176f-6158-42b5-9844-2bed5c6722b6", null, "admin", "ADMIN" },
                    { "af99930b-cabf-4a71-a04f-bd722e5206af", null, "customer", "CUSTOMER" }
                });
        }
    }
}
