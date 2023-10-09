using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventz.Migrations
{
    public partial class editschemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Token",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Person",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Token",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Person",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Person",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Person",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
