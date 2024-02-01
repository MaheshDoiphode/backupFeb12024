using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMS2._0.Migrations
{
    public partial class fourthUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Visitors");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Visitors",
                newName: "VisitorName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisitorName",
                table: "Visitors",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
