using Microsoft.EntityFrameworkCore.Migrations;

namespace PTS.DataAccess.Migrations
{
    public partial class updateTableNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PersonnelId",
                table: "Notifications",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Personnels_PersonnelId",
                table: "Notifications",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Personnels_PersonnelId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PersonnelId",
                table: "Notifications");
        }
    }
}
