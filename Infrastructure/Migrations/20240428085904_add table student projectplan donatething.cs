using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtablestudentprojectplandonatething : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "donateThings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donateThings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_donateThings_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_donateThings_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    birthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Year = table.Column<string>(type: "text", nullable: false),
                    ShirtSize = table.Column<int>(type: "integer", nullable: false),
                    SkirtSize = table.Column<int>(type: "integer", nullable: false),
                    ShoesSize = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_students_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "projectPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DonateThingId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetValue = table.Column<double>(type: "double precision", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projectPlan_AspNetUsers_CreateById",
                        column: x => x.CreateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_projectPlan_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projectPlan_donateThings_DonateThingId",
                        column: x => x.DonateThingId,
                        principalTable: "donateThings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_donateThings_CreateById",
                table: "donateThings",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_donateThings_UpdateById",
                table: "donateThings",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_projectPlan_CreateById",
                table: "projectPlan",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_projectPlan_DonateThingId",
                table: "projectPlan",
                column: "DonateThingId");

            migrationBuilder.CreateIndex(
                name: "IX_projectPlan_UpdateById",
                table: "projectPlan",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_students_CreateById",
                table: "students",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_students_UpdateById",
                table: "students",
                column: "UpdateById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projectPlan");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "donateThings");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
