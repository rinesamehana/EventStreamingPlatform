using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class delGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Film_Company_CompanyId",
                table: "Film");

            migrationBuilder.DropForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Recomandation_RecomandationId",
                table: "Genre");

            migrationBuilder.AlterColumn<int>(
                name: "RecomandationId",
                table: "Genre",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Film",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Company_CompanyId",
                table: "Film",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Recomandation_RecomandationId",
                table: "Genre",
                column: "RecomandationId",
                principalTable: "Recomandation",
                principalColumn: "RecomandationId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Film_Company_CompanyId",
                table: "Film");

            migrationBuilder.DropForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Recomandation_RecomandationId",
                table: "Genre");

            migrationBuilder.AlterColumn<int>(
                name: "RecomandationId",
                table: "Genre",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Film",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Company_CompanyId",
                table: "Film",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Recomandation_RecomandationId",
                table: "Genre",
                column: "RecomandationId",
                principalTable: "Recomandation",
                principalColumn: "RecomandationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
