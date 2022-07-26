using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class addInstitutionIdToAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnionInstitutionId",
                table: "UserAccess",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnionSectionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnionSectionType", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnionSectionType");

            migrationBuilder.DropColumn(
                name: "UnionInstitutionId",
                table: "UserAccess");
        }
    }
}
