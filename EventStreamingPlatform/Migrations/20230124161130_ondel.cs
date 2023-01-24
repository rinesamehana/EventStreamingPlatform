using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class ondel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Country_CountryId",
                table: "Actor");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Country_CountryId",
                table: "Actor",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Country_CountryId",
                table: "Actor");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Country_CountryId",
                table: "Actor",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId");
        }
    }
}
