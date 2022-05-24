using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class newDee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterService_MasterServiceServiceID",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_MasterServiceServiceID",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "MasterServiceServiceID",
                table: "Package");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 17, 2, 38, 821, DateTimeKind.Unspecified).AddTicks(3154), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Package_ServiceID",
                table: "Package",
                column: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_MasterService_ServiceID",
                table: "Package",
                column: "ServiceID",
                principalTable: "MasterService",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterService_ServiceID",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_ServiceID",
                table: "Package");

            migrationBuilder.AddColumn<long>(
                name: "MasterServiceServiceID",
                table: "Package",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 16, 3, 1, 232, DateTimeKind.Unspecified).AddTicks(5717), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Package_MasterServiceServiceID",
                table: "Package",
                column: "MasterServiceServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_MasterService_MasterServiceServiceID",
                table: "Package",
                column: "MasterServiceServiceID",
                principalTable: "MasterService",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
