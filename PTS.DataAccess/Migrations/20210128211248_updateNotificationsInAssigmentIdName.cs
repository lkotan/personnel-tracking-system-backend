using Microsoft.EntityFrameworkCore.Migrations;

namespace PTS.DataAccess.Migrations
{
    public partial class updateNotificationsInAssigmentIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "AssigmentId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AssigmentId",
                table: "Notifications",
                column: "AssigmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Assigments_AssigmentId",
                table: "Notifications",
                column: "AssigmentId",
                principalTable: "Assigments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Assigments_AssigmentId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AssigmentId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AssigmentId",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "AsssigmentId",
                table: "Notifications",
                type: "int",
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
    }
}
