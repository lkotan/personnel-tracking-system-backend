using Microsoft.EntityFrameworkCore.Migrations;

namespace PTS.DataAccess.Migrations
{
    public partial class updateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplates_EmailParameters_EmailParameterId",
                table: "EmailTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Assigments_AssigmentId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelNotifications_Notifications_NotificationId",
                table: "PersonnelNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelNotifications_Personnels_PersonnelId",
                table: "PersonnelNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Roles_RoleId",
                table: "Rules");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplates_EmailParameters_EmailParameterId",
                table: "EmailTemplates",
                column: "EmailParameterId",
                principalTable: "EmailParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Assigments_AssigmentId",
                table: "Notifications",
                column: "AssigmentId",
                principalTable: "Assigments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelNotifications_Notifications_NotificationId",
                table: "PersonnelNotifications",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelNotifications_Personnels_PersonnelId",
                table: "PersonnelNotifications",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Roles_RoleId",
                table: "Rules",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplates_EmailParameters_EmailParameterId",
                table: "EmailTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Assigments_AssigmentId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelNotifications_Notifications_NotificationId",
                table: "PersonnelNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelNotifications_Personnels_PersonnelId",
                table: "PersonnelNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Roles_RoleId",
                table: "Rules");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplates_EmailParameters_EmailParameterId",
                table: "EmailTemplates",
                column: "EmailParameterId",
                principalTable: "EmailParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Assigments_AssigmentId",
                table: "Notifications",
                column: "AssigmentId",
                principalTable: "Assigments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelNotifications_Notifications_NotificationId",
                table: "PersonnelNotifications",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelNotifications_Personnels_PersonnelId",
                table: "PersonnelNotifications",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Roles_RoleId",
                table: "Rules",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
