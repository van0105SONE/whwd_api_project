using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "SkirtSize",
                table: "students",
                newName: "shouldSize");

            migrationBuilder.RenameColumn(
                name: "ShoesSize",
                table: "students",
                newName: "hemSize");

            migrationBuilder.RenameColumn(
                name: "ShirtSize",
                table: "students",
                newName: "chestSize");

            migrationBuilder.RenameColumn(
                name: "GloveSize",
                table: "students",
                newName: "bodyLength");

            migrationBuilder.AddColumn<double>(
                name: "DepositAmount",
                table: "accounts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WithdrawAmount",
                table: "accounts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "donators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Facebook = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_donators_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_donators_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "fundRaisingPlaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaceName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Other = table.Column<string>(type: "text", nullable: true),
                    Facebook = table.Column<string>(type: "text", nullable: true),
                    Longtitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CoordinateById = table.Column<string>(type: "text", nullable: false),
                    villageCode = table.Column<string>(type: "text", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fundRaisingPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fundRaisingPlaces_AspNetUsers_CoordinateById",
                        column: x => x.CoordinateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fundRaisingPlaces_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fundRaisingPlaces_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_fundRaisingPlaces_villages_villageCode",
                        column: x => x.villageCode,
                        principalTable: "villages",
                        principalColumn: "villageCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DonationType = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    DonorById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donation_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donation_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donation_donators_DonorById",
                        column: x => x.DonorById,
                        principalTable: "donators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donation_CreateById",
                table: "Donation",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_DonorById",
                table: "Donation",
                column: "DonorById");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_UpdateById",
                table: "Donation",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_donators_CreateById",
                table: "donators",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_donators_UpdateById",
                table: "donators",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_fundRaisingPlaces_CoordinateById",
                table: "fundRaisingPlaces",
                column: "CoordinateById");

            migrationBuilder.CreateIndex(
                name: "IX_fundRaisingPlaces_CreateById",
                table: "fundRaisingPlaces",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_fundRaisingPlaces_UpdateById",
                table: "fundRaisingPlaces",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_fundRaisingPlaces_villageCode",
                table: "fundRaisingPlaces",
                column: "villageCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "fundRaisingPlaces");

            migrationBuilder.DropTable(
                name: "donators");

            migrationBuilder.DropColumn(
                name: "DepositAmount",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "WithdrawAmount",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "shouldSize",
                table: "students",
                newName: "SkirtSize");

            migrationBuilder.RenameColumn(
                name: "hemSize",
                table: "students",
                newName: "ShoesSize");

            migrationBuilder.RenameColumn(
                name: "chestSize",
                table: "students",
                newName: "ShirtSize");

            migrationBuilder.RenameColumn(
                name: "bodyLength",
                table: "students",
                newName: "GloveSize");

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "accounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
