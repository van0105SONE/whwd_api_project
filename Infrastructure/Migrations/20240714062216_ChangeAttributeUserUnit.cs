using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttributeUserUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "personAmount",
                table: "donateThings");

            migrationBuilder.AddColumn<double>(
                name: "totalPrice",
                table: "donateThings",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "donateThings");

            migrationBuilder.AddColumn<int>(
                name: "personAmount",
                table: "donateThings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
