using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccount24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "sourceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sourceTypes", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNo = table.Column<string>(type: "text", nullable: false),
                    BookingNO = table.Column<string>(type: "text", nullable: false),
                    BankName = table.Column<string>(type: "text", nullable: false),
                    AccountTypesId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnById = table.Column<string>(type: "text", nullable: false),
                    ProjectPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<double>(type: "double precision", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accounts_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_accounts_AspNetUsers_OwnById",
                        column: x => x.OwnById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_accounts_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_accounts_accountTypes_AccountTypesId",
                        column: x => x.AccountTypesId,
                        principalTable: "accountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_accounts_projectPlan_ProjectPlanId",
                        column: x => x.ProjectPlanId,
                        principalTable: "projectPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TransactionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceTypesId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    FromAccount = table.Column<string>(type: "text", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transactions_sourceTypes_SourceTypesId",
                        column: x => x.SourceTypesId,
                        principalTable: "sourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_transactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "transactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_AccountTypesId",
                table: "accounts",
                column: "AccountTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_CreateById",
                table: "accounts",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_OwnById",
                table: "accounts",
                column: "OwnById");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_ProjectPlanId",
                table: "accounts",
                column: "ProjectPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_UpdateById",
                table: "accounts",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_CreateById",
                table: "transactions",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_SourceTypesId",
                table: "transactions",
                column: "SourceTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_TransactionTypeId",
                table: "transactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_UpdateById",
                table: "transactions",
                column: "UpdateById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "accountTypes");

            migrationBuilder.DropTable(
                name: "sourceTypes");

            migrationBuilder.DropTable(
                name: "transactionTypes");
        }
    }
}
