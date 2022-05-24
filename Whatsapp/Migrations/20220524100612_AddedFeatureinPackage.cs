using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedFeatureinPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FeatureID",
                table: "Package",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ServiceFeatureFeatureID",
                table: "Package",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 15, 36, 11, 906, DateTimeKind.Unspecified).AddTicks(6665), new TimeSpan(0, 5, 30, 0, 0)));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterServiceFeatures_ServiceFeatureFeatureID",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_ServiceFeatureFeatureID",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "FeatureID",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "ServiceFeatureFeatureID",
                table: "Package");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 23, 17, 52, 19, 611, DateTimeKind.Unspecified).AddTicks(773), new TimeSpan(0, 5, 30, 0, 0)));
        }
    }
}
