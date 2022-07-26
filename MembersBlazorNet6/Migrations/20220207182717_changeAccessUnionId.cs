using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class changeAccessUnionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                table: "UnionStructure");

            migrationBuilder.DropColumn(
                name: "TypeOfUnionInstitution",
                table: "UserAccess");

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "UserAccess",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "UnionStructure",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_UnionInstitutionId",
                table: "UserAccess",
                column: "UnionInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                table: "UnionStructure",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccess_UnionInstitution_UnionInstitutionId",
                table: "UserAccess",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                table: "UnionStructure");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccess_UnionInstitution_UnionInstitutionId",
                table: "UserAccess");

            migrationBuilder.DropIndex(
                name: "IX_UserAccess_UnionInstitutionId",
                table: "UserAccess");

            migrationBuilder.AlterColumn<string>(
                name: "UnionInstitutionId",
                table: "UserAccess",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfUnionInstitution",
                table: "UserAccess",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "UnionStructure",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UnionStructure_UnionInstitution_UnionInstitutionId",
                table: "UnionStructure",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id");
        }
    }
}
