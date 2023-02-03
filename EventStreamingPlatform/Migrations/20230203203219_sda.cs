using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class sda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SerieId",
                table: "Season",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_SerieId",
                table: "Season",
                column: "SerieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Season_Serie_SerieId",
                table: "Season",
                column: "SerieId",
                principalTable: "Serie",
                principalColumn: "SerieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Season_Serie_SerieId",
                table: "Season");

            migrationBuilder.DropIndex(
                name: "IX_Season_SerieId",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "SerieId",
                table: "Season");
        }
    }
}
