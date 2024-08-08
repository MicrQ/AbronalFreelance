using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbronalFreelance.Server.Migrations
{
    public partial class UpdatedClientModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerEducations_Fields_FieldId",
                table: "FreelancerEducations");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "FreelancerEducations");

            migrationBuilder.DropColumn(
                name: "InstituteName",
                table: "FreelancerEducations");

            migrationBuilder.DropColumn(
                name: "StartingDate",
                table: "FreelancerEducations");

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "FreelancerEducations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TinNo",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompanyEstablishedAt",
                table: "Clients",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerEducations_Fields_FieldId",
                table: "FreelancerEducations",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerEducations_Fields_FieldId",
                table: "FreelancerEducations");

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "FreelancerEducations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "FreelancerEducations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InstituteName",
                table: "FreelancerEducations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingDate",
                table: "FreelancerEducations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "TinNo",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompanyEstablishedAt",
                table: "Clients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerEducations_Fields_FieldId",
                table: "FreelancerEducations",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
