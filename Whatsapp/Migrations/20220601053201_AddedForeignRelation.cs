using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedForeignRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_MasterPackage_MasterPackageId",
                table: "Ledgers");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Ledgers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "MasterPackageId",
                table: "Ledgers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FeatureId",
                table: "Ledgers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 6, 1, 11, 2, 0, 767, DateTimeKind.Unspecified).AddTicks(3594), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_UserFundRequests_UserId",
                table: "UserFundRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_MasterPackage_MasterPackageId",
                table: "Ledgers",
                column: "MasterPackageId",
                principalTable: "MasterPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFundRequests_AspNetUsers_UserId",
                table: "UserFundRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_MasterPackage_MasterPackageId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFundRequests_AspNetUsers_UserId",
                table: "UserFundRequests");

            migrationBuilder.DropIndex(
                name: "IX_UserFundRequests_UserId",
                table: "UserFundRequests");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Ledgers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MasterPackageId",
                table: "Ledgers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FeatureId",
                table: "Ledgers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 26, 15, 27, 53, 129, DateTimeKind.Unspecified).AddTicks(5354), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_MasterPackage_MasterPackageId",
                table: "Ledgers",
                column: "MasterPackageId",
                principalTable: "MasterPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
