using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
namespace project.Migrations
{
    public partial class Timestamp : Migration
    {
        private List<string> tables = new List<string>() { "Activity", "Employee", "Project", "Report", "Tag", "EmployeeProject" };
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            

            tables.ForEach(tableName =>
            {
                migrationBuilder.AddColumn<byte[]>(
                        name : "TimeStamp",
                        table : tableName,
                        type :  "TIMESTAMP",
                        nullable : false
                    );
            });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            tables.ForEach(tableName =>
            {
                migrationBuilder.DropColumn(
                        name : "TimeStamp",
                        table : tableName
                    );
            });
        }
    }
}
