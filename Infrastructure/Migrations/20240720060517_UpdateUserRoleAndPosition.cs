using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRoleAndPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schoools_projectPlan_ProjectId",
                table: "schoools");

            migrationBuilder.DropForeignKey(
                name: "FK_students_projectPlan_ProjectId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_students_schoools_SchoolId",
                table: "students");

            migrationBuilder.DropTable(
                name: "position_teams");

            migrationBuilder.DropColumn(
                name: "RefNO",
                table: "project_teams");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "students",
                newName: "schoolId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "students",
                newName: "projectId");

            migrationBuilder.RenameIndex(
                name: "IX_students_SchoolId",
                table: "students",
                newName: "IX_students_schoolId");

            migrationBuilder.RenameIndex(
                name: "IX_students_ProjectId",
                table: "students",
                newName: "IX_students_projectId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "schoools",
                newName: "projectId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "schoools",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_schoools_ProjectId",
                table: "schoools",
                newName: "IX_schoools_projectId");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "positionId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "projectTeamId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_positionId",
                table: "AspNetUsers",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_projectTeamId",
                table: "AspNetUsers",
                column: "projectTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Position_positionId",
                table: "AspNetUsers",
                column: "positionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_project_teams_projectTeamId",
                table: "AspNetUsers",
                column: "projectTeamId",
                principalTable: "project_teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_schoools_projectPlan_projectId",
                table: "schoools",
                column: "projectId",
                principalTable: "projectPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_projectPlan_projectId",
                table: "students",
                column: "projectId",
                principalTable: "projectPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_schoools_schoolId",
                table: "students",
                column: "schoolId",
                principalTable: "schoools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Position_positionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_project_teams_projectTeamId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_schoools_projectPlan_projectId",
                table: "schoools");

            migrationBuilder.DropForeignKey(
                name: "FK_students_projectPlan_projectId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_students_schoools_schoolId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_positionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_projectTeamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "students");

            migrationBuilder.DropColumn(
                name: "positionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "projectTeamId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "schoolId",
                table: "students",
                newName: "SchoolId");

            migrationBuilder.RenameColumn(
                name: "projectId",
                table: "students",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_students_schoolId",
                table: "students",
                newName: "IX_students_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_students_projectId",
                table: "students",
                newName: "IX_students_ProjectId");

            migrationBuilder.RenameColumn(
                name: "projectId",
                table: "schoools",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "schoools",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_schoools_projectId",
                table: "schoools",
                newName: "IX_schoools_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "RefNO",
                table: "project_teams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "position_teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_position_teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_position_teams_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_position_teams_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_position_teams_project_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "project_teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_position_teams_PositionId",
                table: "position_teams",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_position_teams_TeamId",
                table: "position_teams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_position_teams_UserId",
                table: "position_teams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_schoools_projectPlan_ProjectId",
                table: "schoools",
                column: "ProjectId",
                principalTable: "projectPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_projectPlan_ProjectId",
                table: "students",
                column: "ProjectId",
                principalTable: "projectPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_schoools_SchoolId",
                table: "students",
                column: "SchoolId",
                principalTable: "schoools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
