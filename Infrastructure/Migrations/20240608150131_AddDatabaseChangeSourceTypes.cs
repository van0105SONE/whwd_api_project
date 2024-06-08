using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseChangeSourceTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_sourceTypes_SourceTypesId",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_SourceTypesId",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "FromAccount",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "SourceTypesId",
                table: "transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "SourceTypesId",
                table: "Donation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Donation_SourceTypesId",
                table: "Donation",
                column: "SourceTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_sourceTypes_SourceTypesId",
                table: "Donation",
                column: "SourceTypesId",
                principalTable: "sourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_sourceTypes_SourceTypesId",
                table: "Donation");

            migrationBuilder.DropIndex(
                name: "IX_Donation_SourceTypesId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "SourceTypesId",
                table: "Donation");

            migrationBuilder.AddColumn<string>(
                name: "FromAccount",
                table: "transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SourceTypesId",
                table: "transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_transactions_SourceTypesId",
                table: "transactions",
                column: "SourceTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_sourceTypes_SourceTypesId",
                table: "transactions",
                column: "SourceTypesId",
                principalTable: "sourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
