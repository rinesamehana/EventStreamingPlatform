using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class updateSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Serie",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Serie",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SerieActors",
                columns: table => new
                {
                    SerieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieActors", x => new { x.SerieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_SerieActors_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SerieActors_Serie_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Serie",
                        principalColumn: "SerieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SerieGenres",
                columns: table => new
                {
                    SerieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieGenres", x => new { x.SerieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_SerieGenres_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SerieGenres_Serie_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Serie",
                        principalColumn: "SerieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SerieMainActors",
                columns: table => new
                {
                    SerieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieMainActors", x => new { x.SerieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_SerieMainActors_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SerieMainActors_Serie_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Serie",
                        principalColumn: "SerieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Serie_CompanyId",
                table: "Serie",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Serie_LanguageId",
                table: "Serie",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieActors_ActorId",
                table: "SerieActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieGenres_GenreId",
                table: "SerieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieMainActors_ActorId",
                table: "SerieMainActors",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Serie_Company_CompanyId",
                table: "Serie",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Serie_Language_LanguageId",
                table: "Serie",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Serie_Company_CompanyId",
                table: "Serie");

            migrationBuilder.DropForeignKey(
                name: "FK_Serie_Language_LanguageId",
                table: "Serie");

            migrationBuilder.DropTable(
                name: "SerieActors");

            migrationBuilder.DropTable(
                name: "SerieGenres");

            migrationBuilder.DropTable(
                name: "SerieMainActors");

            migrationBuilder.DropIndex(
                name: "IX_Serie_CompanyId",
                table: "Serie");

            migrationBuilder.DropIndex(
                name: "IX_Serie_LanguageId",
                table: "Serie");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Serie");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Serie");
        }
    }
}
