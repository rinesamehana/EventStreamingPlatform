using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class ratingsssdss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Episode_EpisodeId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EpisodesId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Episode_EpisodeId",
                table: "Comments",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "EpisodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Episode_EpisodeId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "EpisodesId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Episode_EpisodeId",
                table: "Comments",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "EpisodeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
