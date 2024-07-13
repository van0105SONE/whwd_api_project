using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAccType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_accountTypes_AccountTypesId",
                table: "accounts");

            migrationBuilder.DropTable(
                name: "accountTypes");

            migrationBuilder.DropIndex(
                name: "IX_accounts_AccountTypesId",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "AccountTypesId",
                table: "accounts");

            migrationBuilder.AddColumn<string>(
                name: "AccountTypes",
                table: "accounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountTypes",
                table: "accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountTypesId",
                table: "accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "accountTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accountTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_AccountTypesId",
                table: "accounts",
                column: "AccountTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_accountTypes_AccountTypesId",
                table: "accounts",
                column: "AccountTypesId",
                principalTable: "accountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
