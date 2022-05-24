using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedFeatureinPackages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FeatureID",
                table: "Package",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 15, 43, 35, 59, DateTimeKind.Unspecified).AddTicks(8459), new TimeSpan(0, 5, 30, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FeatureID",
                table: "Package",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 15, 36, 11, 906, DateTimeKind.Unspecified).AddTicks(6665), new TimeSpan(0, 5, 30, 0, 0)));
        }
    }
}
