using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbronalFreelance.Server.Migrations
{
    public partial class ModifyingSkillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FreelancerSkills");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "FreelancerSkills");

            migrationBuilder.DropColumn(
                name: "skilledAt",
                table: "FreelancerSkills");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FreelancerSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "FreelancerSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "skilledAt",
                table: "FreelancerSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
