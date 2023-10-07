using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventz.Migrations
{
    public partial class editTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Person_PersonIDId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PersonIDId",
                table: "Users",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PersonIDId",
                table: "Users",
                newName: "IX_Users_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Person_PersonId",
                table: "Users",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Person_PersonId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Users",
                newName: "PersonIDId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                newName: "IX_Users_PersonIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Person_PersonIDId",
                table: "Users",
                column: "PersonIDId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
