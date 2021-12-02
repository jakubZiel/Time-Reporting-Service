using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace project.Migrations
{
    public partial class Timestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                    name: "Timestamp",
                    table: "Activity",
                    type: "datetime2",
                    nullable: true
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                    name: "Timestamp",
                    table: "Activity"
                );
        }
    }
}
