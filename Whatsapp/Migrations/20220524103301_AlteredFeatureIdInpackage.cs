using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AlteredFeatureIdInpackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterServiceFeatures_FeatureID",
                table: "Package");

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
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 16, 3, 1, 232, DateTimeKind.Unspecified).AddTicks(5717), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Package_MasterServiceFeatures_FeatureID",
                table: "Package",
                column: "FeatureID",
                principalTable: "MasterServiceFeatures",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_MasterServiceFeatures_FeatureID",
                table: "Package");

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
                value: new DateTimeOffset(new DateTime(2022, 5, 24, 15, 56, 12, 343, DateTimeKind.Unspecified).AddTicks(6714), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Package_MasterServiceFeatures_FeatureID",
                table: "Package",
                column: "FeatureID",
                principalTable: "MasterServiceFeatures",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
