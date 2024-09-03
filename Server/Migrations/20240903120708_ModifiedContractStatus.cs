using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbronalFreelance.Server.Migrations
{
    public partial class ModifiedContractStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractStatuses_ContractStatusTypes_ContractStatusTypeId",
                table: "ContractStatuses");

            migrationBuilder.DropTable(
                name: "ContractStatusTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ContractStatuses");

            migrationBuilder.RenameColumn(
                name: "ContractStatusTypeId",
                table: "ContractStatuses",
                newName: "ApprovalStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractStatuses_ContractStatusTypeId",
                table: "ContractStatuses",
                newName: "IX_ContractStatuses_ApprovalStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractStatuses_ApprovalStatuses_ApprovalStatusId",
                table: "ContractStatuses",
                column: "ApprovalStatusId",
                principalTable: "ApprovalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractStatuses_ApprovalStatuses_ApprovalStatusId",
                table: "ContractStatuses");

            migrationBuilder.RenameColumn(
                name: "ApprovalStatusId",
                table: "ContractStatuses",
                newName: "ContractStatusTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractStatuses_ApprovalStatusId",
                table: "ContractStatuses",
                newName: "IX_ContractStatuses_ContractStatusTypeId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ContractStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ContractStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatusTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContractStatuses_ContractStatusTypes_ContractStatusTypeId",
                table: "ContractStatuses",
                column: "ContractStatusTypeId",
                principalTable: "ContractStatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
