using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class bachChangePhoneUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccess_AspNetUsers_UserId",
                table: "UserAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccess_UnionInstitution_UnionInstitutionId",
                table: "UserAccess");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAccess",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "UserAccess",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccess_AspNetUsers_UserId",
                table: "UserAccess",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
                name: "FK_UserAccess_AspNetUsers_UserId",
                table: "UserAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccess_UnionInstitution_UnionInstitutionId",
                table: "UserAccess");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAccess",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "UserAccess",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccess_AspNetUsers_UserId",
                table: "UserAccess",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccess_UnionInstitution_UnionInstitutionId",
                table: "UserAccess",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
