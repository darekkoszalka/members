using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class addUnionInstitutionContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnionInstitutionContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnionInstitutionId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WWW = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkWww = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnionInstitutionContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnionInstitutionContact_UnionInstitution_UnionInstitutionId",
                        column: x => x.UnionInstitutionId,
                        principalTable: "UnionInstitution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnionInstitutionContact_UnionInstitutionId",
                table: "UnionInstitutionContact",
                column: "UnionInstitutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnionInstitutionContact");
        }
    }
}
