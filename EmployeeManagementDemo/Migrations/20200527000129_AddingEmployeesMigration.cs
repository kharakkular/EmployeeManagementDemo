using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementDemo.Migrations
{
    public partial class AddingEmployeesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Department", "Email", "Name" },
                values: new object[] { 2, 1, "kul@gmail.com", "Kulwant" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
