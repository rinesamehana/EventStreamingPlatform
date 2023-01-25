using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class upsdds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SerieId",
                table: "Episode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episode_SerieId",
                table: "Episode",
                column: "SerieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Serie_SerieId",
                table: "Episode",
                column: "SerieId",
                principalTable: "Serie",
                principalColumn: "SerieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Serie_SerieId",
                table: "Episode");

            migrationBuilder.DropIndex(
                name: "IX_Episode_SerieId",
                table: "Episode");

            migrationBuilder.DropColumn(
                name: "SerieId",
                table: "Episode");
        }
    }
}
