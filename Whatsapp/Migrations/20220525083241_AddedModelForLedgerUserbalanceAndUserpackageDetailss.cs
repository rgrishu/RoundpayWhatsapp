using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedModelForLedgerUserbalanceAndUserpackageDetailss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ledgers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    MasterPackageId = table.Column<long>(nullable: false),
                    ServiceId = table.Column<long>(nullable: false),
                    MasterServiceServiceID = table.Column<long>(nullable: true),
                    FeatureId = table.Column<long>(nullable: false),
                    MasterServiceFeaturesFeatureID = table.Column<long>(nullable: true),
                    OpeningBalance = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ClosingBalance = table.Column<double>(nullable: false),
                    TransactionType = table.Column<string>(nullable: false),
                    EntryBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ledgers_MasterPackage_MasterPackageId",
                        column: x => x.MasterPackageId,
                        principalTable: "MasterPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ledgers_MasterServiceFeatures_MasterServiceFeaturesFeatureID",
                        column: x => x.MasterServiceFeaturesFeatureID,
                        principalTable: "MasterServiceFeatures",
                        principalColumn: "FeatureID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ledgers_MasterService_MasterServiceServiceID",
                        column: x => x.MasterServiceServiceID,
                        principalTable: "MasterService",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBalance",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Balance = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBalance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPackageDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    MasterPackageId = table.Column<long>(nullable: false),
                    EntryBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPackageDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPackageDetail_MasterPackage_MasterPackageId",
                        column: x => x.MasterPackageId,
                        principalTable: "MasterPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 25, 14, 2, 41, 116, DateTimeKind.Unspecified).AddTicks(6124), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_MasterPackageId",
                table: "Ledgers",
                column: "MasterPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_MasterServiceFeaturesFeatureID",
                table: "Ledgers",
                column: "MasterServiceFeaturesFeatureID");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_MasterServiceServiceID",
                table: "Ledgers",
                column: "MasterServiceServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPackageDetail_MasterPackageId",
                table: "UserPackageDetail",
                column: "MasterPackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ledgers");

            migrationBuilder.DropTable(
                name: "UserBalance");

            migrationBuilder.DropTable(
                name: "UserPackageDetail");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 5, 25, 14, 0, 4, 633, DateTimeKind.Unspecified).AddTicks(2273), new TimeSpan(0, 5, 30, 0, 0)));
        }
    }
}
