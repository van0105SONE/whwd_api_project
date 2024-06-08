using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "schoools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    villageCode = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schoools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schoools_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schoools_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_schoools_projectPlan_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projectPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schoools_villages_villageCode",
                        column: x => x.villageCode,
                        principalTable: "villages",
                        principalColumn: "villageCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_schoools_CreateById",
                table: "schoools",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_schoools_ProjectId",
                table: "schoools",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_schoools_UpdateById",
                table: "schoools",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_schoools_villageCode",
                table: "schoools",
                column: "villageCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schoools");
        }
    }
}
