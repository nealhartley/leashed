using Microsoft.EntityFrameworkCore.Migrations;

namespace leashApi.Migrations
{
    public partial class PicturestoUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserDataId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_UserDataId",
                table: "Pictures",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_UserData_UserDataId",
                table: "Pictures",
                column: "UserDataId",
                principalTable: "UserData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_UserData_UserDataId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_UserDataId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "Pictures");
        }
    }
}
