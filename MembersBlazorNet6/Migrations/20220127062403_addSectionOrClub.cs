using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class addSectionOrClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                table: "UnionStructure");

            migrationBuilder.DropForeignKey(
                name: "FK_UnionStructureFunction_UnionFuncionType_UnionFunctionTypeId",
                table: "UnionStructureFunction");

            migrationBuilder.AlterColumn<int>(
                name: "UnionFunctionTypeId",
                table: "UnionStructureFunction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "UnionStructure",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UnionSectionOrClubId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnionSectionOrClub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnionInstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnionSectionOrClub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnionSectionOrClub_UnionInstitution_UnionInstitutionId",
                        column: x => x.UnionInstitutionId,
                        principalTable: "UnionInstitution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_UnionSectionOrClubId",
                table: "Member",
                column: "UnionSectionOrClubId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionSectionOrClub_UnionInstitutionId",
                table: "UnionSectionOrClub",
                column: "UnionInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_UnionSectionOrClub_UnionSectionOrClubId",
                table: "Member",
                column: "UnionSectionOrClubId",
                principalTable: "UnionSectionOrClub",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                table: "UnionStructure",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnionStructureFunction_UnionFuncionType_UnionFunctionTypeId",
                table: "UnionStructureFunction",
                column: "UnionFunctionTypeId",
                principalTable: "UnionFuncionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_UnionSectionOrClub_UnionSectionOrClubId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                table: "UnionStructure");

            migrationBuilder.DropForeignKey(
                name: "FK_UnionStructureFunction_UnionFuncionType_UnionFunctionTypeId",
                table: "UnionStructureFunction");

            migrationBuilder.DropTable(
                name: "UnionSectionOrClub");

            migrationBuilder.DropIndex(
                name: "IX_Member_UnionSectionOrClubId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "UnionSectionOrClubId",
                table: "Member");

            migrationBuilder.AlterColumn<int>(
                name: "UnionFunctionTypeId",
                table: "UnionStructureFunction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "UnionStructure",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                table: "UnionStructure",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnionStructureFunction_UnionFuncionType_UnionFunctionTypeId",
                table: "UnionStructureFunction",
                column: "UnionFunctionTypeId",
                principalTable: "UnionFuncionType",
                principalColumn: "Id");
        }
    }
}
