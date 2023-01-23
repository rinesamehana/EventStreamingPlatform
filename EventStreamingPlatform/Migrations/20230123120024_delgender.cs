using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class delgender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Gender_GenderId",
                table: "Actor");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Gender_GenderId",
                table: "Actor",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Gender_GenderId",
                table: "Actor");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Gender_GenderId",
                table: "Actor",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "GenderId");
        }
    }
}
