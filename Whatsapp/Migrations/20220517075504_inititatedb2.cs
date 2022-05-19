using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class inititatedb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockoutEnd", "NormalizedEmail" },
                values: new object[] { new DateTimeOffset(new DateTime(2022, 5, 17, 13, 25, 3, 197, DateTimeKind.Unspecified).AddTicks(8676), new TimeSpan(0, 5, 30, 0, 0)), "ADMIN@GMAIL.COM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WID",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockoutEnd", "NormalizedEmail" },
                values: new object[] { new DateTimeOffset(new DateTime(2022, 5, 17, 12, 34, 54, 665, DateTimeKind.Unspecified).AddTicks(8735), new TimeSpan(0, 5, 30, 0, 0)), null });
        }
    }
}
