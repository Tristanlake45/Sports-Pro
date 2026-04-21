using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsPro.Migrations
{
    public partial class SeedRegistrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "CustomerID", "ProductID" },
                values: new object[] { 1002, 1 });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "CustomerID", "ProductID" },
                values: new object[] { 1002, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumns: new[] { "CustomerID", "ProductID" },
                keyValues: new object[] { 1002, 1 });

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumns: new[] { "CustomerID", "ProductID" },
                keyValues: new object[] { 1002, 3 });
        }
    }
}
