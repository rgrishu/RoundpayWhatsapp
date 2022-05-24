using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedFeatureinsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterServiceFeatures_ServiceFeatureFeatureID",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_ServiceFeatureFeatureID",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "ServiceFeatureFeatureID",
                table: "Package");

            migrationBuilder.AlterColumn<long>(
                name: "FeatureID",
                table: "Package",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 15, 56, 12, 343, DateTimeKind.Unspecified).AddTicks(6714), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Package_FeatureID",
                table: "Package",
                column: "FeatureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_MasterServiceFeatures_FeatureID",
                table: "Package",
                column: "FeatureID",
                principalTable: "MasterServiceFeatures",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterServiceFeatures_FeatureID",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_FeatureID",
                table: "Package");

            migrationBuilder.AlterColumn<long>(
                name: "FeatureID",
                table: "Package",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "ServiceFeatureFeatureID",
                table: "Package",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 15, 43, 35, 59, DateTimeKind.Unspecified).AddTicks(8459), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Package_ServiceFeatureFeatureID",
                table: "Package",
                column: "ServiceFeatureFeatureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_MasterServiceFeatures_ServiceFeatureFeatureID",
                table: "Package",
                column: "ServiceFeatureFeatureID",
                principalTable: "MasterServiceFeatures",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
