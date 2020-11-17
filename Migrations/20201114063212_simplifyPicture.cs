using Microsoft.EntityFrameworkCore.Migrations;

namespace leashApi.Migrations
{
    public partial class simplifyPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_UserData_UserDataId",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "UserDataId",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Pictures",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "UserDataId",
                table: "Pictures",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_UserData_UserDataId",
                table: "Pictures",
                column: "UserDataId",
                principalTable: "UserData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
