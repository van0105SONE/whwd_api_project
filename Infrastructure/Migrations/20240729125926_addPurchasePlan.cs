using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPurchasePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    totalQty = table.Column<string>(type: "text", nullable: false),
                    totalPrice = table.Column<double>(type: "double precision", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cart_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "cartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    qty = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    CartId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cartItems_cart_CartId",
                        column: x => x.CartId,
                        principalTable: "cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_CreateById",
                table: "cart",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_cart_UpdateById",
                table: "cart",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_CartId",
                table: "cartItems",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartItems");

            migrationBuilder.DropTable(
                name: "cart");
        }
    }
}
