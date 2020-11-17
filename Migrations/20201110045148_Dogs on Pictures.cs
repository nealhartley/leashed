using Microsoft.EntityFrameworkCore.Migrations;

namespace leashApi.Migrations
{
    public partial class DogsonPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Dogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_PictureId",
                table: "Dogs",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Pictures_PictureId",
                table: "Dogs",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Pictures_PictureId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_PictureId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Dogs");
        }
    }
}
