using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class reportsv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcceptedTime",
                table: "Activity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportedTime",
                table: "Activity",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedTime",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "ReportedTime",
                table: "Activity");
        }
    }
}
