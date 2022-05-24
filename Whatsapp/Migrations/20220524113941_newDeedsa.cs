using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class newDeedsa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterService_ServiceID",
                table: "Package");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceID",
                table: "Package",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 17, 9, 40, 860, DateTimeKind.Unspecified).AddTicks(4653), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Package_MasterService_ServiceID",
                table: "Package",
                column: "ServiceID",
                principalTable: "MasterService",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterService_ServiceID",
                table: "Package");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceID",
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
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 17, 2, 38, 821, DateTimeKind.Unspecified).AddTicks(3154), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Package_MasterService_ServiceID",
                table: "Package",
                column: "ServiceID",
                principalTable: "MasterService",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
