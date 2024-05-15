using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_PositionTeam_PositionTeamId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserType_UserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionTeam_positions_PositionId",
                table: "PositionTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionTeam_project_teams_TeamId",
                table: "PositionTeam");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_PositionTeamId",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_positions",
                table: "positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserType",
                table: "UserType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PositionTeam",
                table: "PositionTeam");

            migrationBuilder.DropColumn(
                name: "PositionTeamId",
                table: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "positions",
                newName: "Position");

            migrationBuilder.RenameTable(
                name: "UserType",
                newName: "userTypes");

            migrationBuilder.RenameTable(
                name: "PositionTeam",
                newName: "position_teams");

            migrationBuilder.RenameIndex(
                name: "IX_PositionTeam_TeamId",
                table: "position_teams",
                newName: "IX_position_teams_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_PositionTeam_PositionId",
                table: "position_teams",
                newName: "IX_position_teams_PositionId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "position_teams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Position",
                table: "Position",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userTypes",
                table: "userTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_position_teams",
                table: "position_teams",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_position_teams_UserId",
                table: "position_teams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_userTypes_UserTypeId",
                table: "AspNetUsers",
                column: "UserTypeId",
                principalTable: "userTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_position_teams_AspNetUsers_UserId",
                table: "position_teams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_position_teams_Position_PositionId",
                table: "position_teams",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_position_teams_project_teams_TeamId",
                table: "position_teams",
                column: "TeamId",
                principalTable: "project_teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_userTypes_UserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_position_teams_AspNetUsers_UserId",
                table: "position_teams");

            migrationBuilder.DropForeignKey(
                name: "FK_position_teams_Position_PositionId",
                table: "position_teams");

            migrationBuilder.DropForeignKey(
                name: "FK_position_teams_project_teams_TeamId",
                table: "position_teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userTypes",
                table: "userTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_position_teams",
                table: "position_teams");

            migrationBuilder.DropIndex(
                name: "IX_position_teams_UserId",
                table: "position_teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Position",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "position_teams");

            migrationBuilder.RenameTable(
                name: "userTypes",
                newName: "UserType");

            migrationBuilder.RenameTable(
                name: "position_teams",
                newName: "PositionTeam");

            migrationBuilder.RenameTable(
                name: "Position",
                newName: "positions");

            migrationBuilder.RenameIndex(
                name: "IX_position_teams_TeamId",
                table: "PositionTeam",
                newName: "IX_PositionTeam_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_position_teams_PositionId",
                table: "PositionTeam",
                newName: "IX_PositionTeam_PositionId");

            migrationBuilder.AddColumn<Guid>(
                name: "PositionTeamId",
                table: "AspNetRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserType",
                table: "UserType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PositionTeam",
                table: "PositionTeam",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_positions",
                table: "positions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_PositionTeamId",
                table: "AspNetRoles",
                column: "PositionTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_PositionTeam_PositionTeamId",
                table: "AspNetRoles",
                column: "PositionTeamId",
                principalTable: "PositionTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserType_UserTypeId",
                table: "AspNetUsers",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionTeam_positions_PositionId",
                table: "PositionTeam",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionTeam_project_teams_TeamId",
                table: "PositionTeam",
                column: "TeamId",
                principalTable: "project_teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
