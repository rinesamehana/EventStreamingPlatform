using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Genders_GenderId",
                table: "Actor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genders",
                table: "Genders");

            migrationBuilder.RenameTable(
                name: "Genders",
                newName: "Gender");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Film",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gender",
                table: "Gender",
                column: "GenderId");

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISO_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_LanguageId",
                table: "Film",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Gender_GenderId",
                table: "Actor",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Gender_GenderId",
                table: "Actor");

            migrationBuilder.DropForeignKey(
                name: "FK_Film_Language_LanguageId",
                table: "Film");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Film_LanguageId",
                table: "Film");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gender",
                table: "Gender");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Film");

            migrationBuilder.RenameTable(
                name: "Gender",
                newName: "Genders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genders",
                table: "Genders",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Genders_GenderId",
                table: "Actor",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId");
        }
    }
}
