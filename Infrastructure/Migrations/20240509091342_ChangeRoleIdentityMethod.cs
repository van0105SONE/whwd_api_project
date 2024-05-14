using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoleIdentityMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_users_teams_UsersTeamsId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "users_teams");

            migrationBuilder.RenameColumn(
                name: "UsersTeamsId",
                table: "AspNetRoles",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_UsersTeamsId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_PositionId");

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RefNo = table.Column<string>(type: "text", nullable: false),
                    PositionName = table.Column<string>(type: "text", nullable: false),
                    ProjectTeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_positions_project_teams_ProjectTeamId",
                        column: x => x.ProjectTeamId,
                        principalTable: "project_teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_positions_ProjectTeamId",
                table: "positions",
                column: "ProjectTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_positions_PositionId",
                table: "AspNetRoles",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_positions_PositionId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "AspNetRoles",
                newName: "UsersTeamsId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_PositionId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_UsersTeamsId");

            migrationBuilder.CreateTable(
                name: "users_teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_teams_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_teams_project_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "project_teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_teams_TeamId",
                table: "users_teams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_users_teams_UserId",
                table: "users_teams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_users_teams_UsersTeamsId",
                table: "AspNetRoles",
                column: "UsersTeamsId",
                principalTable: "users_teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
