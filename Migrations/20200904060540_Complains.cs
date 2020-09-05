using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CityProblemSolver.Migrations
{
    public partial class Complains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complains",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(nullable: false),
                    ComplainDate = table.Column<DateTime>(type: "date", nullable: true),
                    SolvedDescription = table.Column<string>(nullable: true),
                    SolvedDate = table.Column<DateTime>(type: "date", nullable: true),
                    Type = table.Column<bool>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complains", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Complains_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complains_AreaId",
                table: "Complains",
                column: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complains");
        }
    }
}
