using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConjoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "conjoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JoinerId = table.Column<string>(type: "text", nullable: false),
                    JointFundRaising = table.Column<bool>(type: "boolean", nullable: false),
                    JointCashCalculate = table.Column<bool>(type: "boolean", nullable: false),
                    FundRaisingPlaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conjoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_conjoints_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conjoints_AspNetUsers_JoinerId",
                        column: x => x.JoinerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conjoints_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_conjoints_fundRaisingPlaces_FundRaisingPlaceId",
                        column: x => x.FundRaisingPlaceId,
                        principalTable: "fundRaisingPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_conjoints_CreateById",
                table: "conjoints",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_conjoints_FundRaisingPlaceId",
                table: "conjoints",
                column: "FundRaisingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_conjoints_JoinerId",
                table: "conjoints",
                column: "JoinerId");

            migrationBuilder.CreateIndex(
                name: "IX_conjoints_UpdateById",
                table: "conjoints",
                column: "UpdateById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "conjoints");
        }
    }
}
