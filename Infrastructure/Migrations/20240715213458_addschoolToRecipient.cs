using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addschoolToRecipient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_students_SchoolId",
                table: "students",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_schoools_SchoolId",
                table: "students",
                column: "SchoolId",
                principalTable: "schoools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_schoools_SchoolId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_SchoolId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "students");
        }
    }
}
