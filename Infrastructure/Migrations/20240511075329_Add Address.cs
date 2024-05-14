using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Village_BornVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Village_CurrentVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Village",
                table: "Village");

            migrationBuilder.RenameTable(
                name: "Village",
                newName: "villages");

            migrationBuilder.AddColumn<string>(
                name: "districtCode",
                table: "villages",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_villages",
                table: "villages",
                column: "villageCode");

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    ProvinceCode = table.Column<string>(type: "text", nullable: false),
                    ProvinceName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinces", x => x.ProvinceCode);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    districtCode = table.Column<string>(type: "text", nullable: false),
                    districtName = table.Column<string>(type: "text", nullable: false),
                    villageProvinceCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.districtCode);
                    table.ForeignKey(
                        name: "FK_districts_provinces_villageProvinceCode",
                        column: x => x.villageProvinceCode,
                        principalTable: "provinces",
                        principalColumn: "ProvinceCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_villages_districtCode",
                table: "villages",
                column: "districtCode");

            migrationBuilder.CreateIndex(
                name: "IX_districts_villageProvinceCode",
                table: "districts",
                column: "villageProvinceCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_villages_BornVillagevillageCode",
                table: "AspNetUsers",
                column: "BornVillagevillageCode",
                principalTable: "villages",
                principalColumn: "villageCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_villages_CurrentVillagevillageCode",
                table: "AspNetUsers",
                column: "CurrentVillagevillageCode",
                principalTable: "villages",
                principalColumn: "villageCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_villages_districts_districtCode",
                table: "villages",
                column: "districtCode",
                principalTable: "districts",
                principalColumn: "districtCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_villages_BornVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_villages_CurrentVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_villages_districts_districtCode",
                table: "villages");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_villages",
                table: "villages");

            migrationBuilder.DropIndex(
                name: "IX_villages_districtCode",
                table: "villages");

            migrationBuilder.DropColumn(
                name: "districtCode",
                table: "villages");

            migrationBuilder.RenameTable(
                name: "villages",
                newName: "Village");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Village",
                table: "Village",
                column: "villageCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Village_BornVillagevillageCode",
                table: "AspNetUsers",
                column: "BornVillagevillageCode",
                principalTable: "Village",
                principalColumn: "villageCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Village_CurrentVillagevillageCode",
                table: "AspNetUsers",
                column: "CurrentVillagevillageCode",
                principalTable: "Village",
                principalColumn: "villageCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
