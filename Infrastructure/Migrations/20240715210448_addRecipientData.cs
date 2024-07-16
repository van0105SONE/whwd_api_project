using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addRecipientData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bodyLength",
                table: "students");

            migrationBuilder.DropColumn(
                name: "chestSize",
                table: "students");

            migrationBuilder.DropColumn(
                name: "hemSize",
                table: "students");

            migrationBuilder.DropColumn(
                name: "shouldSize",
                table: "students");

            migrationBuilder.AddColumn<string>(
                name: "shirtSize",
                table: "students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "shoesSize",
                table: "students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "skirtSize",
                table: "students",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shirtSize",
                table: "students");

            migrationBuilder.DropColumn(
                name: "shoesSize",
                table: "students");

            migrationBuilder.DropColumn(
                name: "skirtSize",
                table: "students");

            migrationBuilder.AddColumn<int>(
                name: "bodyLength",
                table: "students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "chestSize",
                table: "students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "hemSize",
                table: "students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "shouldSize",
                table: "students",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
