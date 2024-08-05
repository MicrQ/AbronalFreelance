using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbronalFreelance.Server.Migrations
{
    public partial class freelancerFieldRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FreelancerFields_FieldId",
                table: "FreelancerFields",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerFields_Fields_FieldId",
                table: "FreelancerFields",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerFields_Fields_FieldId",
                table: "FreelancerFields");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerFields_FieldId",
                table: "FreelancerFields");
        }
    }
}
