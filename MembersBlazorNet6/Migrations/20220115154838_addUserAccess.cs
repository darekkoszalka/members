using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class addUserAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserAccess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnionInstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccess_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccess_UnionInstitution_UnionInstitutionId",
                        column: x => x.UnionInstitutionId,
                        principalTable: "UnionInstitution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_UnionInstitutionId",
                table: "UserAccess",
                column: "UnionInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_UserId",
                table: "UserAccess",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccess");

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
    }
}
