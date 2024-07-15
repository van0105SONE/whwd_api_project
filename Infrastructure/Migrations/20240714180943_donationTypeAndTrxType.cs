using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class donationTypeAndTrxType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_sourceTypes_SourceTypesId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_transactionTypes_TransactionTypeId",
                table: "transactions");

            migrationBuilder.DropTable(
                name: "transactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_transactions_TransactionTypeId",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_Donation_SourceTypesId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "SourceTypesId",
                table: "Donation");

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "transactions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionTypeId",
                table: "transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SourceTypesId",
                table: "Donation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "transactionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_TransactionTypeId",
                table: "transactions",
                column: "TransactionTypeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_transactionTypes_TransactionTypeId",
                table: "transactions",
                column: "TransactionTypeId",
                principalTable: "transactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
