using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class filmcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FilmId",
                table: "Comments",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Film_FilmId",
                table: "Comments",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Film_FilmId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_FilmId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Comments");
        }
    }
}
