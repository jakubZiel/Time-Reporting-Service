using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class projects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ID", "Active", "Description", "Name", "OwnerID", "TimeBudget" },
                values: new object[] { 1, true, "Some React fullstack application", "ReactApp", 1, 1500 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ID", "Active", "Description", "Name", "OwnerID", "TimeBudget" },
                values: new object[] { 2, true, "Some Vue.Js frontend application", "VueApp", 2, 2200 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ID", "Active", "Description", "Name", "OwnerID", "TimeBudget" },
                values: new object[] { 3, true, "Some Spring Boot backend application", "Spring Boot App", 4, 1600 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
