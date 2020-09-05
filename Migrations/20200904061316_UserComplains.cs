using Microsoft.EntityFrameworkCore.Migrations;

namespace CityProblemSolver.Migrations
{
    public partial class UserComplains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComplains_Complains_ComplainID",
                table: "UserComplains");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "UserComplains");

            migrationBuilder.RenameColumn(
                name: "ComplainID",
                table: "UserComplains",
                newName: "ComplainId");

            migrationBuilder.RenameIndex(
                name: "IX_UserComplains_ComplainID",
                table: "UserComplains",
                newName: "IX_UserComplains_ComplainId");

            migrationBuilder.AlterColumn<int>(
                name: "ComplainId",
                table: "UserComplains",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComplains_Complains_ComplainId",
                table: "UserComplains",
                column: "ComplainId",
                principalTable: "Complains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComplains_Complains_ComplainId",
                table: "UserComplains");

            migrationBuilder.RenameColumn(
                name: "ComplainId",
                table: "UserComplains",
                newName: "ComplainID");

            migrationBuilder.RenameIndex(
                name: "IX_UserComplains_ComplainId",
                table: "UserComplains",
                newName: "IX_UserComplains_ComplainID");

            migrationBuilder.AlterColumn<int>(
                name: "ComplainID",
                table: "UserComplains",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "UserComplains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComplains_Complains_ComplainID",
                table: "UserComplains",
                column: "ComplainID",
                principalTable: "Complains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
