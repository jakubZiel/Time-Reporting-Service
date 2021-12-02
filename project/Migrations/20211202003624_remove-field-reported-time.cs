using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class removefieldreportedtime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportedTime",
                table: "Activity");

            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 2, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportedTime",
                table: "Activity",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
