using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbronalFreelance.Server.Migrations
{
    public partial class NewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            // Drop the existing column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Profiles");

            // Add the new column with the correct type
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Add the primary key constraint back
            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the existing primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            // Drop the new column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Profiles");

            // Add the old column back
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // Add the primary key constraint back
            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");
        }
    }
}
