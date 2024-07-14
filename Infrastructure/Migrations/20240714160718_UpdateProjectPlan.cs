using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueInBath",
                table: "projectPlan");

            migrationBuilder.RenameColumn(
                name: "valueInKip",
                table: "projectPlan",
                newName: "TotalRecieve");

            migrationBuilder.RenameColumn(
                name: "ValueInDollar",
                table: "projectPlan",
                newName: "TotalFund");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalRecieve",
                table: "projectPlan",
                newName: "valueInKip");

            migrationBuilder.RenameColumn(
                name: "TotalFund",
                table: "projectPlan",
                newName: "ValueInDollar");

            migrationBuilder.AddColumn<double>(
                name: "ValueInBath",
                table: "projectPlan",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
