using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class addUnionStructureFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnionStructureId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnionStructure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnionInstitutionId = table.Column<int>(type: "int", nullable: false),
                    UnionStructureTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnionStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                        column: x => x.UnionInstitutionId,
                        principalTable: "UnionInstitution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnionStructure_UnionStructureType_UnionStructureTypeId",
                        column: x => x.UnionStructureTypeId,
                        principalTable: "UnionStructureType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnionStructureFunction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnionStructureId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    UnionFunctionTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnionStructureFunction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnionStructureFunction_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnionStructureFunction_UnionFuncionType_UnionFunctionTypeId",
                        column: x => x.UnionFunctionTypeId,
                        principalTable: "UnionFuncionType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnionStructureFunction_UnionStructure_UnionStructureId",
                        column: x => x.UnionStructureId,
                        principalTable: "UnionStructure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_UnionStructureId",
                table: "Member",
                column: "UnionStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionStructure_UnionInstitutionId",
                table: "UnionStructure",
                column: "UnionInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionStructure_UnionStructureTypeId",
                table: "UnionStructure",
                column: "UnionStructureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionStructureFunction_MemberId",
                table: "UnionStructureFunction",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionStructureFunction_UnionFunctionTypeId",
                table: "UnionStructureFunction",
                column: "UnionFunctionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionStructureFunction_UnionStructureId",
                table: "UnionStructureFunction",
                column: "UnionStructureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_UnionStructure_UnionStructureId",
                table: "Member",
                column: "UnionStructureId",
                principalTable: "UnionStructure",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_UnionStructure_UnionStructureId",
                table: "Member");

            migrationBuilder.DropTable(
                name: "UnionStructureFunction");

            migrationBuilder.DropTable(
                name: "UnionStructure");

            migrationBuilder.DropIndex(
                name: "IX_Member_UnionStructureId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "UnionStructureId",
                table: "Member");
        }
    }
}
