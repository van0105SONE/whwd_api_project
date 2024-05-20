using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "students",
                newName: "Level");

            migrationBuilder.AddColumn<int>(
                name: "GloveSize",
                table: "students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_students_ProjectId",
                table: "students",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_projectPlan_ProjectId",
                table: "students",
                column: "ProjectId",
                principalTable: "projectPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_projectPlan_ProjectId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_ProjectId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "GloveSize",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "students",
                newName: "Year");
        }
    }
}
