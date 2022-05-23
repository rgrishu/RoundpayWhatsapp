using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class addedChargeAblesinPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Charge",
                table: "Package",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChargeAble",
                table: "Package",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 23, 16, 41, 8, 877, DateTimeKind.Unspecified).AddTicks(160), new TimeSpan(0, 5, 30, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Charge",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "IsChargeAble",
                table: "Package");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 23, 15, 30, 14, 125, DateTimeKind.Unspecified).AddTicks(7468), new TimeSpan(0, 5, 30, 0, 0)));
        }
    }
}
