using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class activities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Project_ProjectID",
                table: "Activity");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "ID", "AcceptedTime", "DateCreated", "Description", "DurationMinutes", "EmployeeID", "Frozen", "Name", "ProjectID", "ReportID", "ReportedTime", "Tag" },
                values: new object[] { 1, null, new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Local), "checking if everything is ok with the API", 30, 1, false, "API debugging", 1, null, null, "debugging" });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "ID", "Name", "ProjectID" },
                values: new object[,]
                {
                    { 1, "coding", 1 },
                    { 2, "debuging", 1 },
                    { 3, "database", 2 },
                    { 4, "coding", 2 },
                    { 5, "drinking", 3 },
                    { 6, "coding", 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Project_ProjectID",
                table: "Activity",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Project_ProjectID",
                table: "Activity");

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Project_ProjectID",
                table: "Activity",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
