using Microsoft.EntityFrameworkCore.Migrations;

namespace PTS.DataAccess.Migrations
{
    public partial class updateTableAssigmentAndNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AsssigmentId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AsssigmentId",
                table: "Notifications",
                column: "AsssigmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Assigments_AsssigmentId",
                table: "Notifications",
                column: "AsssigmentId",
                principalTable: "Assigments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Assigments_AsssigmentId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AsssigmentId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AsssigmentId",
                table: "Notifications");
        }
    }
}
