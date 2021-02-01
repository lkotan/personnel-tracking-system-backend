using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PTS.DataAccess.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssigmentTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    TagColor = table.Column<string>(maxLength: 7, nullable: false, defaultValueSql: "space(0)"),
                    TagBackgroundColor = table.Column<string>(maxLength: 7, nullable: false, defaultValueSql: "space(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigmentTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    Message = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    IsBlocked = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "Convert(Date,GetDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    IsBlocked = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ApplicationModule = table.Column<int>(nullable: false),
                    View = table.Column<bool>(nullable: false, defaultValue: false),
                    Insert = table.Column<bool>(nullable: false, defaultValue: false),
                    Update = table.Column<bool>(nullable: false, defaultValue: false),
                    Delete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rules_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: true),
                    TitleId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    PersonnelType = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 25, nullable: false, defaultValueSql: "space(0)"),
                    LastName = table.Column<string>(maxLength: 25, nullable: false, defaultValueSql: "space(0)"),
                    Email = table.Column<string>(maxLength: 75, nullable: false, defaultValueSql: "space(0)"),
                    Gsm = table.Column<string>(maxLength: 11, nullable: false, defaultValueSql: "space(0)"),
                    ProfilePhoto = table.Column<string>(maxLength: 255, nullable: false, defaultValueSql: "space(0)"),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    RefreshToken = table.Column<string>(maxLength: 255, nullable: false, defaultValueSql: "space(0)"),
                    RefreshTokenExpiredDate = table.Column<DateTime>(nullable: false, defaultValueSql: "Convert(Date,GetDate())"),
                    IsBlocked = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnels_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(nullable: false),
                    NotificationId = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountNotifications_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountNotifications_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assigments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(nullable: false),
                    AssigmentTagId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    HtmlContent = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    Priority = table.Column<short>(nullable: false, defaultValue: (short)0),
                    CreatedUser = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "Convert(Date,GetDate())"),
                    DueDate = table.Column<DateTime>(nullable: false, defaultValueSql: "Convert(Date,GetDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assigments_AssigmentTags_AssigmentTagId",
                        column: x => x.AssigmentTagId,
                        principalTable: "AssigmentTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assigments_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Personnels",
                columns: new[] { "Id", "DepartmentId", "Email", "FirstName", "Gsm", "LastName", "PasswordHash", "PasswordSalt", "PersonnelType", "ProfilePhoto", "RefreshToken", "RefreshTokenExpiredDate", "RoleId", "TitleId" },
                values: new object[] { 1, null, "lutfikotann@gmail.com", "Lütfi", "", "Kotan", new byte[] { 57, 195, 173, 176, 24, 8, 59, 69, 18, 103, 242, 228, 230, 22, 180, 7, 125, 12, 157, 92, 138, 137, 227, 241, 43, 168, 167, 23, 163, 49, 217, 93, 4, 106, 172, 229, 86, 119, 169, 36, 104, 207, 156, 76, 181, 172, 79, 184, 86, 152, 190, 237, 1, 25, 67, 19, 17, 170, 73, 224, 58, 62, 229, 176 }, new byte[] { 72, 148, 185, 144, 95, 207, 116, 162, 95, 110, 232, 40, 41, 139, 7, 224, 147, 184, 53, 137, 23, 111, 4, 128, 76, 48, 99, 217, 85, 187, 25, 210, 92, 110, 225, 167, 35, 210, 254, 214, 123, 85, 238, 220, 240, 58, 173, 13, 171, 249, 55, 130, 99, 39, 155, 56, 37, 35, 118, 5, 48, 19, 78, 4, 152, 116, 25, 120, 43, 104, 33, 72, 179, 144, 150, 41, 177, 163, 106, 31, 254, 249, 45, 164, 128, 249, 159, 67, 158, 39, 40, 147, 114, 170, 122, 83, 144, 63, 46, 137, 7, 250, 95, 152, 39, 209, 145, 170, 238, 22, 153, 105, 45, 108, 122, 235, 207, 2, 163, 220, 213, 112, 51, 200, 150, 241, 122, 182 }, 20, "", "Y9ADLGNJ8ZO4T9GHFKUP6UVOB6W0FLSKFHUHU2XM", new DateTime(2021, 1, 23, 16, 31, 12, 516, DateTimeKind.Local).AddTicks(8310), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AccountNotifications_NotificationId",
                table: "AccountNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountNotifications_PersonnelId_NotificationId",
                table: "AccountNotifications",
                columns: new[] { "PersonnelId", "NotificationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assigments_AssigmentTagId",
                table: "Assigments",
                column: "AssigmentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Assigments_PersonnelId",
                table: "Assigments",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_DepartmentId",
                table: "Personnels",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_Email",
                table: "Personnels",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_RefreshToken",
                table: "Personnels",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_RoleId",
                table: "Personnels",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_TitleId",
                table: "Personnels",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_RoleId_ApplicationModule",
                table: "Rules",
                columns: new[] { "RoleId", "ApplicationModule" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountNotifications");

            migrationBuilder.DropTable(
                name: "Assigments");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "AssigmentTags");

            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Titles");
        }
    }
}
