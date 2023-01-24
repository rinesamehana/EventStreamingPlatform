using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class actorcity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Actor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_CityId",
                table: "Actor",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_City_CityId",
                table: "Actor",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_City_CityId",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_CityId",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Actor");
        }
    }
}
