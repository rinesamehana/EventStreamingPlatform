using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class dellan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "LanguageId");
        }
    }
}
