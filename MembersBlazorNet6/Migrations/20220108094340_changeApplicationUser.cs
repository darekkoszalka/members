using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class changeApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnionIntitutionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UnionIntitutionId",
                table: "AspNetUsers",
                column: "UnionIntitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UnionInstitution_UnionIntitutionId",
                table: "AspNetUsers",
                column: "UnionIntitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UnionInstitution_UnionIntitutionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UnionIntitutionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UnionIntitutionId",
                table: "AspNetUsers");
        }
    }
}
