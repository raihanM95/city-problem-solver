using Microsoft.EntityFrameworkCore.Migrations;

namespace CityProblemSolver.Migrations
{
    public partial class WorkerComplains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkerComplains",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ComplainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerComplains", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkerComplains_Complains_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "Complains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerComplains_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerComplains_ComplainId",
                table: "WorkerComplains",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerComplains_UserId",
                table: "WorkerComplains",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkerComplains");
        }
    }
}
