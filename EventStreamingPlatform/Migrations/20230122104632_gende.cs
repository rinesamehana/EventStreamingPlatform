using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class gende : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Genders_GenderId",
                table: "Actor");

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "Actor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Genders_GenderId",
                table: "Actor",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Genders_GenderId",
                table: "Actor");

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "Actor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Genders_GenderId",
                table: "Actor",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
