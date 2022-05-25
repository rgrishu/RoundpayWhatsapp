using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedModelForLedgerUserbalanceAndUserpackageDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 25, 14, 0, 4, 633, DateTimeKind.Unspecified).AddTicks(2273), new TimeSpan(0, 5, 30, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 17, 9, 40, 860, DateTimeKind.Unspecified).AddTicks(4653), new TimeSpan(0, 5, 30, 0, 0)));
        }
    }
}
