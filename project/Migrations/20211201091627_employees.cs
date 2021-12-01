using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class employees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "ID", "Name", "Password", "Surname" },
                values: new object[,]
                {
                    { 1, "Jakub", "123", "Zielinski" },
                    { 2, "Piotr", "1234", "Lewandowski" },
                    { 3, "Waldemar", "12345", "Grabski" },
                    { 4, "Krzysztof", "123456", "Chabko" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
