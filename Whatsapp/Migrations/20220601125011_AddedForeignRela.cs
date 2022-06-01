using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedForeignRela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_MasterServiceFeatures_MasterServiceFeaturesFeatureID",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_MasterService_MasterServiceServiceID",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_MasterServiceFeaturesFeatureID",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_MasterServiceServiceID",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "MasterServiceFeaturesFeatureID",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "MasterServiceServiceID",
                table: "Ledgers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 6, 1, 18, 20, 11, 8, DateTimeKind.Unspecified).AddTicks(5194), new TimeSpan(0, 5, 30, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MasterServiceFeaturesFeatureID",
                table: "Ledgers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MasterServiceServiceID",
                table: "Ledgers",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 6, 1, 13, 29, 35, 258, DateTimeKind.Unspecified).AddTicks(6044), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_MasterServiceFeaturesFeatureID",
                table: "Ledgers",
                column: "MasterServiceFeaturesFeatureID");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_MasterServiceServiceID",
                table: "Ledgers",
                column: "MasterServiceServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_MasterServiceFeatures_MasterServiceFeaturesFeatureID",
                table: "Ledgers",
                column: "MasterServiceFeaturesFeatureID",
                principalTable: "MasterServiceFeatures",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_MasterService_MasterServiceServiceID",
                table: "Ledgers",
                column: "MasterServiceServiceID",
                principalTable: "MasterService",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
