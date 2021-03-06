﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PTS.DataAccess.Migrations
{
    public partial class createRowAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Personnels",
                columns: new[] { "Id", "DepartmentId", "Email", "FirstName", "Gsm", "LastName", "PasswordHash", "PasswordSalt", "PersonnelType", "ProfilePhoto", "RefreshToken", "RefreshTokenExpiredDate", "RoleId", "TitleId" },
                values: new object[] { 1, null, "lutfikotann@gmail.com", "Lütfi", "", "Kotan", new byte[] { 200, 236, 39, 134, 189, 196, 162, 8, 197, 189, 79, 243, 13, 10, 177, 46, 59, 153, 131, 183, 245, 106, 120, 132, 228, 225, 22, 122, 149, 162, 57, 176, 59, 71, 172, 185, 133, 156, 232, 123, 134, 119, 54, 195, 88, 66, 37, 174, 128, 54, 56, 35, 117, 94, 205, 62, 247, 227, 68, 110, 23, 10, 142, 72 }, new byte[] { 82, 166, 247, 192, 255, 119, 24, 99, 40, 64, 33, 92, 31, 200, 220, 17, 154, 52, 63, 123, 238, 165, 224, 168, 8, 100, 197, 183, 145, 88, 58, 83, 130, 255, 24, 180, 108, 117, 69, 25, 250, 178, 213, 219, 201, 179, 255, 27, 180, 25, 118, 158, 173, 204, 230, 209, 243, 250, 127, 218, 50, 235, 113, 145, 17, 136, 163, 224, 90, 8, 30, 36, 10, 53, 92, 74, 36, 58, 166, 78, 173, 91, 80, 228, 192, 242, 120, 118, 33, 51, 173, 176, 30, 79, 19, 28, 214, 67, 152, 81, 249, 192, 253, 163, 91, 151, 144, 192, 96, 233, 14, 199, 42, 251, 155, 180, 39, 118, 81, 19, 159, 239, 192, 156, 68, 122, 44, 45 }, 20, "", "BJEVVGOU6NCUMZPJMXIAFTDN6JPVNCCKTPGLZZGO", new DateTime(2021, 1, 23, 18, 11, 14, 596, DateTimeKind.Local).AddTicks(5954), null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
