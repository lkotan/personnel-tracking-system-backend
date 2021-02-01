using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PTS.DataAccess.Migrations
{
    public partial class addTableEmailTemplateAndEmailParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "EmailParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    SmtpServer = table.Column<string>(maxLength: 255, nullable: false, defaultValueSql: "space(0)"),
                    UserName = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    Password = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "space(0)"),
                    Port = table.Column<int>(nullable: false),
                    EnableSsl = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailParameterId = table.Column<int>(nullable: false),
                    TemplateType = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    MessageTemplate = table.Column<string>(nullable: false, defaultValueSql: "space(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_EmailParameters_EmailParameterId",
                        column: x => x.EmailParameterId,
                        principalTable: "EmailParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EmailParameterId",
                table: "EmailTemplates",
                column: "EmailParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_TemplateType",
                table: "EmailTemplates",
                column: "TemplateType",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "EmailParameters");

            migrationBuilder.InsertData(
                table: "Personnels",
                columns: new[] { "Id", "DepartmentId", "Email", "FirstName", "Gsm", "LastName", "PasswordHash", "PasswordSalt", "PersonnelType", "ProfilePhoto", "RefreshToken", "RefreshTokenExpiredDate", "RoleId", "TitleId" },
                values: new object[] { 1, null, "lutfikotann@gmail.com", "Lütfi", "", "Kotan", new byte[] { 57, 195, 173, 176, 24, 8, 59, 69, 18, 103, 242, 228, 230, 22, 180, 7, 125, 12, 157, 92, 138, 137, 227, 241, 43, 168, 167, 23, 163, 49, 217, 93, 4, 106, 172, 229, 86, 119, 169, 36, 104, 207, 156, 76, 181, 172, 79, 184, 86, 152, 190, 237, 1, 25, 67, 19, 17, 170, 73, 224, 58, 62, 229, 176 }, new byte[] { 72, 148, 185, 144, 95, 207, 116, 162, 95, 110, 232, 40, 41, 139, 7, 224, 147, 184, 53, 137, 23, 111, 4, 128, 76, 48, 99, 217, 85, 187, 25, 210, 92, 110, 225, 167, 35, 210, 254, 214, 123, 85, 238, 220, 240, 58, 173, 13, 171, 249, 55, 130, 99, 39, 155, 56, 37, 35, 118, 5, 48, 19, 78, 4, 152, 116, 25, 120, 43, 104, 33, 72, 179, 144, 150, 41, 177, 163, 106, 31, 254, 249, 45, 164, 128, 249, 159, 67, 158, 39, 40, 147, 114, 170, 122, 83, 144, 63, 46, 137, 7, 250, 95, 152, 39, 209, 145, 170, 238, 22, 153, 105, 45, 108, 122, 235, 207, 2, 163, 220, 213, 112, 51, 200, 150, 241, 122, 182 }, 20, "", "Y9ADLGNJ8ZO4T9GHFKUP6UVOB6W0FLSKFHUHU2XM", new DateTime(2021, 1, 23, 16, 31, 12, 516, DateTimeKind.Local).AddTicks(8310), null, null });
        }
    }
}
