using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUserType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_generations_UserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_generations",
                table: "generations");

            migrationBuilder.RenameTable(
                name: "generations",
                newName: "UserType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserType",
                table: "UserType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserType_UserTypeId",
                table: "AspNetUsers",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserType_UserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserType",
                table: "UserType");

            migrationBuilder.RenameTable(
                name: "UserType",
                newName: "generations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_generations",
                table: "generations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_generations_UserTypeId",
                table: "AspNetUsers",
                column: "UserTypeId",
                principalTable: "generations",
                principalColumn: "Id");
        }
    }
}
