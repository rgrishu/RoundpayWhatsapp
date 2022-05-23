using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class addedChargeAblesin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChargeAble",
                table: "Package");

            migrationBuilder.AddColumn<bool>(
                name: "IsChargeAfterHitExceed",
                table: "Package",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDirectChargeable",
                table: "Package",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 23, 17, 52, 19, 611, DateTimeKind.Unspecified).AddTicks(773), new TimeSpan(0, 5, 30, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChargeAfterHitExceed",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "IsDirectChargeable",
                table: "Package");

            migrationBuilder.AddColumn<bool>(
                name: "IsChargeAble",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 23, 16, 41, 8, 877, DateTimeKind.Unspecified).AddTicks(160), new TimeSpan(0, 5, 30, 0, 0)));
        }
    }
}
