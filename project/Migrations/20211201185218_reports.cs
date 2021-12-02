using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class reports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedTime",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "ReportedTime",
                table: "Activity");

            migrationBuilder.RenameColumn(
                name: "month",
                table: "Report",
                newName: "Month");

            migrationBuilder.AddColumn<bool>(
                name: "Frozen",
                table: "Report",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frozen",
                table: "Report");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "Report",
                newName: "month");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedTime",
                table: "Activity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportedTime",
                table: "Activity",
                type: "datetime2",
                nullable: true);
        }
    }
}
