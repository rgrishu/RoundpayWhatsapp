using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class fffd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 7, 8, 11, 8, 49, 733, DateTimeKind.Unspecified).AddTicks(8582), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.InsertData(
                table: "WABAsProviders",
                columns: new[] { "Id", "APICode", "APIId", "IsActive", "ProviderName" },
                values: new object[] { 2, "PAYTM", 2, true, "PAYTM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WABAsProviders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 7, 8, 11, 6, 4, 908, DateTimeKind.Unspecified).AddTicks(1058), new TimeSpan(0, 5, 30, 0, 0)));
        }
    }
}
