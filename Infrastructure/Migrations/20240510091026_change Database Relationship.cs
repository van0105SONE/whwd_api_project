using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeDatabaseRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_majors_MajorId",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_faculty_departments_DepartmentId",
                table: "faculty");

            migrationBuilder.DropForeignKey(
                name: "FK_university_faculty_FacultyId",
                table: "university");

            migrationBuilder.DropIndex(
                name: "IX_university_FacultyId",
                table: "university");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "university");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "faculty",
                newName: "universityId");

            migrationBuilder.RenameIndex(
                name: "IX_faculty_DepartmentId",
                table: "faculty",
                newName: "IX_faculty_universityId");

            migrationBuilder.RenameColumn(
                name: "MajorId",
                table: "departments",
                newName: "facultyId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_MajorId",
                table: "departments",
                newName: "IX_departments_facultyId");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "majors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "BornVillagevillageCode",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentVillagevillageCode",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Village",
                columns: table => new
                {
                    villageCode = table.Column<string>(type: "text", nullable: false),
                    villageName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Village", x => x.villageCode);
                });

            migrationBuilder.CreateIndex(
                name: "IX_majors_DepartmentId",
                table: "majors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BornVillagevillageCode",
                table: "AspNetUsers",
                column: "BornVillagevillageCode");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentVillagevillageCode",
                table: "AspNetUsers",
                column: "CurrentVillagevillageCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Village_BornVillagevillageCode",
                table: "AspNetUsers",
                column: "BornVillagevillageCode",
                principalTable: "Village",
                principalColumn: "villageCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Village_CurrentVillagevillageCode",
                table: "AspNetUsers",
                column: "CurrentVillagevillageCode",
                principalTable: "Village",
                principalColumn: "villageCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_faculty_facultyId",
                table: "departments",
                column: "facultyId",
                principalTable: "faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_faculty_university_universityId",
                table: "faculty",
                column: "universityId",
                principalTable: "university",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_majors_departments_DepartmentId",
                table: "majors",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Village_BornVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Village_CurrentVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_faculty_facultyId",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_faculty_university_universityId",
                table: "faculty");

            migrationBuilder.DropForeignKey(
                name: "FK_majors_departments_DepartmentId",
                table: "majors");

            migrationBuilder.DropTable(
                name: "Village");

            migrationBuilder.DropIndex(
                name: "IX_majors_DepartmentId",
                table: "majors");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BornVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "majors");

            migrationBuilder.DropColumn(
                name: "BornVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentVillagevillageCode",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "universityId",
                table: "faculty",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_faculty_universityId",
                table: "faculty",
                newName: "IX_faculty_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "facultyId",
                table: "departments",
                newName: "MajorId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_facultyId",
                table: "departments",
                newName: "IX_departments_MajorId");

            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "university",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_university_FacultyId",
                table: "university",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_majors_MajorId",
                table: "departments",
                column: "MajorId",
                principalTable: "majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_faculty_departments_DepartmentId",
                table: "faculty",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_university_faculty_FacultyId",
                table: "university",
                column: "FacultyId",
                principalTable: "faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
