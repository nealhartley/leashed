using Microsoft.EntityFrameworkCore.Migrations;

namespace leashApi.Migrations
{
    public partial class DogUserandDogsPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "UserDataId",
                table: "Dogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PictureDogJoins",
                columns: table => new
                {
                    PictureId = table.Column<int>(nullable: false),
                    DogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureDogJoins", x => new { x.PictureId, x.DogId });
                    table.ForeignKey(
                        name: "FK_PictureDogJoins_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PictureDogJoins_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PictureDogJoins_DogId",
                table: "PictureDogJoins",
                column: "DogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureDogJoins");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "Dogs");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Dogs",
                type: "integer",
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
    }
}
