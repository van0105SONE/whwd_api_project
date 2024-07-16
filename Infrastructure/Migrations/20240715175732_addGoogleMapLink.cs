using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addGoogleMapLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "fundRaisingPlaces");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "fundRaisingPlaces");

            migrationBuilder.AddColumn<string>(
                name: "googleMapLink",
                table: "fundRaisingPlaces",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "googleMapLink",
                table: "fundRaisingPlaces");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "fundRaisingPlaces",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "fundRaisingPlaces",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
