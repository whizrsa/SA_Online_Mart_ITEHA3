using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SA_Online_Mart.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3f47a63-92c4-432f-8146-6cdb54e4f4e2",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "customer", "CUSTOMER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3f47a63-92c4-432f-8146-6cdb54e4f4e2",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "staff", "STAFF" });
        }
    }
}
