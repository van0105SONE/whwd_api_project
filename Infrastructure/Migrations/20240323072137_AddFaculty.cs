using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFaculty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_majors_departments_DepartmentId",
                table: "majors");

            migrationBuilder.DropColumn(
                name: "YearEnd",
                table: "generations");

            migrationBuilder.DropColumn(
                name: "YearStart",
                table: "generations");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "majors",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_majors_DepartmentId",
                table: "majors",
                newName: "IX_majors_FacultyId");

            migrationBuilder.CreateTable(
                name: "faculty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faculty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_faculty_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_faculty_DepartmentId",
                table: "faculty",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_majors_faculty_FacultyId",
                table: "majors",
                column: "FacultyId",
                principalTable: "faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_majors_faculty_FacultyId",
                table: "majors");

            migrationBuilder.DropTable(
                name: "faculty");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "majors",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_majors_FacultyId",
                table: "majors",
                newName: "IX_majors_DepartmentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "YearEnd",
                table: "generations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "YearStart",
                table: "generations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_majors_departments_DepartmentId",
                table: "majors",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
