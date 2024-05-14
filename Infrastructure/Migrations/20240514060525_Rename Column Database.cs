using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_districts_provinces_villageProvinceCode",
                table: "districts");

            migrationBuilder.RenameColumn(
                name: "villageProvinceCode",
                table: "districts",
                newName: "ProvinceCode");

            migrationBuilder.RenameIndex(
                name: "IX_districts_villageProvinceCode",
                table: "districts",
                newName: "IX_districts_ProvinceCode");

            migrationBuilder.AddForeignKey(
                name: "FK_districts_provinces_ProvinceCode",
                table: "districts",
                column: "ProvinceCode",
                principalTable: "provinces",
                principalColumn: "ProvinceCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_districts_provinces_ProvinceCode",
                table: "districts");

            migrationBuilder.RenameColumn(
                name: "ProvinceCode",
                table: "districts",
                newName: "villageProvinceCode");

            migrationBuilder.RenameIndex(
                name: "IX_districts_ProvinceCode",
                table: "districts",
                newName: "IX_districts_villageProvinceCode");

            migrationBuilder.AddForeignKey(
                name: "FK_districts_provinces_villageProvinceCode",
                table: "districts",
                column: "villageProvinceCode",
                principalTable: "provinces",
                principalColumn: "ProvinceCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
