using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventz.Migrations
{
    public partial class alter_fields_time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeDescription",
                table: "Event",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Event",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Event",
                newName: "TimeDescription");
        }
    }
}
