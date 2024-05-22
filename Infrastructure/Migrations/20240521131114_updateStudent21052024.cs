using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateStudent21052024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Level",
                table: "students",
                newName: "level");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "students",
                newName: "lname");

            migrationBuilder.AddColumn<string>(
                name: "fname",
                table: "students",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fname",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "level",
                table: "students",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "lname",
                table: "students",
                newName: "Name");
        }
    }
}
