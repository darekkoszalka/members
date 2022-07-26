using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembersBlazorNet6.Migrations
{
    public partial class addWorkStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_UnionInstitution_UnionInstitutionId",
                table: "Member");

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "Member",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Member",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pesel",
                table: "Member",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnionCard",
                table: "Member",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_EducationId",
                table: "Member",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Education_EducationId",
                table: "Member",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_UnionInstitution_UnionInstitutionId",
                table: "Member",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Education_EducationId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_UnionInstitution_UnionInstitutionId",
                table: "Member");

            migrationBuilder.DropTable(
                name: "WorkStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Member_EducationId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "UnionCard",
                table: "Member");

            migrationBuilder.AlterColumn<int>(
                name: "UnionInstitutionId",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Member",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Pesel",
                table: "Member",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_UnionInstitution_UnionInstitutionId",
                table: "Member",
                column: "UnionInstitutionId",
                principalTable: "UnionInstitution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
