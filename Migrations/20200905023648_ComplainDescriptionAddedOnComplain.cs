using Microsoft.EntityFrameworkCore.Migrations;

namespace CityProblemSolver.Migrations
{
    public partial class ComplainDescriptionAddedOnComplain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComplainDescription",
                table: "Complains",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplainDescription",
                table: "Complains");
        }
    }
}
