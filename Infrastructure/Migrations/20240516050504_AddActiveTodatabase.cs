using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveTodatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projectPlan_donateThings_DonateThingId",
                table: "projectPlan");

            migrationBuilder.DropIndex(
                name: "IX_projectPlan_DonateThingId",
                table: "projectPlan");

            migrationBuilder.DropColumn(
                name: "DonateThingId",
                table: "projectPlan");

            migrationBuilder.RenameColumn(
                name: "TargetValue",
                table: "projectPlan",
                newName: "valueInKip");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "projectPlan",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "ValueInBath",
                table: "projectPlan",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueInDollar",
                table: "projectPlan",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectPlanId",
                table: "donateThings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_donateThings_ProjectPlanId",
                table: "donateThings",
                column: "ProjectPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_donateThings_projectPlan_ProjectPlanId",
                table: "donateThings",
                column: "ProjectPlanId",
                principalTable: "projectPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_donateThings_projectPlan_ProjectPlanId",
                table: "donateThings");

            migrationBuilder.DropIndex(
                name: "IX_donateThings_ProjectPlanId",
                table: "donateThings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "projectPlan");

            migrationBuilder.DropColumn(
                name: "ValueInBath",
                table: "projectPlan");

            migrationBuilder.DropColumn(
                name: "ValueInDollar",
                table: "projectPlan");

            migrationBuilder.DropColumn(
                name: "ProjectPlanId",
                table: "donateThings");

            migrationBuilder.RenameColumn(
                name: "valueInKip",
                table: "projectPlan",
                newName: "TargetValue");

            migrationBuilder.AddColumn<Guid>(
                name: "DonateThingId",
                table: "projectPlan",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_projectPlan_DonateThingId",
                table: "projectPlan",
                column: "DonateThingId");

            migrationBuilder.AddForeignKey(
                name: "FK_projectPlan_donateThings_DonateThingId",
                table: "projectPlan",
                column: "DonateThingId",
                principalTable: "donateThings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
