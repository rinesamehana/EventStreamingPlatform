using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class serieEpis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Serie_SerieId",
                table: "Episode");

            migrationBuilder.AlterColumn<int>(
                name: "SerieId",
                table: "Episode",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SeasonId",
                table: "Episode",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "SerieId",
                table: "Episode",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SeasonId",
                table: "Episode",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Serie_SerieId",
                table: "Episode",
                column: "SerieId",
                principalTable: "Serie",
                principalColumn: "SerieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
