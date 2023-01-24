using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class addcountryToActor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Actor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_CountryId",
                table: "Actor",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Country_CountryId",
                table: "Actor",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Country_CountryId",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_CountryId",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Actor");
        }
    }
}
