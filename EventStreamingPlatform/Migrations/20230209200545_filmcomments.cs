using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class filmcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Episode_EpisodeId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Film_FilmId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Episode_EpisodeId",
                table: "Comments",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "EpisodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Film_FilmId",
                table: "Comments",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Episode_EpisodeId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Film_FilmId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Episode_EpisodeId",
                table: "Comments",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "EpisodeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Film_FilmId",
                table: "Comments",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "ID");
        }
    }
}
