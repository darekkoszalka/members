using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class changeUserAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccess_UnionInstitution_UnionInstitutionId",
                table: "UserAccess");

            migrationBuilder.DropIndex(
                name: "IX_UserAccess_UnionInstitutionId",
                table: "UserAccess");

            migrationBuilder.DropColumn(
                name: "UnionInstitutionId",
                table: "UserAccess");

            migrationBuilder.AddColumn<string>(
                name: "Access",
                table: "UserAccess",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Access",
                table: "UserAccess");

            migrationBuilder.AddColumn<int>(
                name: "UnionInstitutionId",
                table: "UserAccess",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_UnionInstitutionId",
                table: "UserAccess",
                column: "UnionInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccess_UnionInstitution_UnionInstitutionId",
                table: "UserAccess",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id");
        }
    }
}
