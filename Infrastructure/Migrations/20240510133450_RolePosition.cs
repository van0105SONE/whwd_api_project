using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RolePosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_positions_PositionId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_positions_project_teams_ProjectTeamId",
                table: "positions");

            migrationBuilder.DropIndex(
                name: "IX_positions_ProjectTeamId",
                table: "positions");

            migrationBuilder.DropColumn(
                name: "ProjectTeamId",
                table: "positions");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "AspNetRoles",
                newName: "PositionTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_PositionId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_PositionTeamId");

            migrationBuilder.CreateTable(
                name: "PositionTeam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionTeam_positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionTeam_project_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "project_teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PositionTeam_PositionId",
                table: "PositionTeam",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionTeam_TeamId",
                table: "PositionTeam",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_PositionTeam_PositionTeamId",
                table: "AspNetRoles",
                column: "PositionTeamId",
                principalTable: "PositionTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_PositionTeam_PositionTeamId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PositionTeam");

            migrationBuilder.RenameColumn(
                name: "PositionTeamId",
                table: "AspNetRoles",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_PositionTeamId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_PositionId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectTeamId",
                table: "positions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_positions_project_teams_ProjectTeamId",
                table: "positions",
                column: "ProjectTeamId",
                principalTable: "project_teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
