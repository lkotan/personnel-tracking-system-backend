using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PTS.DataAccess.Migrations
{
    public partial class updateTablePersonnels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Personnels",
                columns: new[] { "Id", "DepartmentId", "Email", "FirstName", "Gsm", "LastName", "PasswordHash", "PasswordSalt", "PersonnelType", "ProfilePhoto", "RefreshToken", "RefreshTokenExpiredDate", "RoleId", "TitleId" },
                values: new object[] { 1, null, "lutfikotann@gmail.com", "Lütfi", "", "Kotan", new byte[] { 49, 104, 242, 91, 108, 55, 21, 91, 252, 115, 48, 161, 125, 227, 239, 135, 147, 103, 34, 59, 195, 255, 48, 105, 119, 27, 119, 93, 81, 95, 40, 48, 226, 93, 194, 215, 31, 20, 28, 114, 85, 99, 99, 181, 5, 188, 115, 113, 64, 49, 172, 134, 124, 58, 162, 181, 170, 162, 92, 18, 67, 62, 53, 124 }, new byte[] { 229, 31, 77, 33, 226, 18, 153, 234, 71, 147, 143, 118, 173, 199, 149, 109, 254, 113, 229, 33, 228, 131, 222, 88, 201, 225, 52, 72, 25, 34, 161, 215, 86, 239, 137, 54, 247, 75, 126, 167, 150, 226, 159, 32, 123, 156, 243, 233, 68, 18, 112, 57, 122, 115, 84, 166, 7, 156, 20, 117, 198, 29, 172, 120, 207, 161, 1, 153, 175, 221, 12, 213, 17, 243, 237, 184, 18, 27, 234, 179, 134, 147, 39, 161, 17, 147, 210, 198, 173, 60, 218, 6, 82, 82, 194, 192, 202, 184, 239, 23, 73, 34, 216, 121, 53, 76, 137, 91, 128, 63, 86, 59, 25, 17, 163, 195, 146, 94, 26, 13, 144, 170, 74, 223, 54, 128, 156, 177 }, 20, "", "NGJIIMFNCMHV8NLPR2YJ42ZW9KFZTH8SHOGT0NLZO", new DateTime(2021, 1, 24, 17, 22, 11, 997, DateTimeKind.Local).AddTicks(8683), null, null });
        }
    }
}
