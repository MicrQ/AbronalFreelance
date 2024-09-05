using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbronalFreelance.Server.Migrations
{
    public partial class LocationTypeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationTypeId",
                table: "Locations",
                column: "LocationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationTypes_LocationTypeId",
                table: "Locations",
                column: "LocationTypeId",
                principalTable: "LocationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationTypes_LocationTypeId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_LocationTypeId",
                table: "Locations");
        }
    }
}
