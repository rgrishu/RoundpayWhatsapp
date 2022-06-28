using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedWABAModelAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WABAsModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APIId = table.Column<int>(nullable: false),
                    APICode = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WABAsModels", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 6, 28, 11, 41, 59, 623, DateTimeKind.Unspecified).AddTicks(8282), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.InsertData(
                table: "WABAsModels",
                columns: new[] { "Id", "APICode", "APIId", "IsActive", "ProviderName" },
                values: new object[] { 1, "RNDPAY", 1, true, "ROUNDPAY1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WABAsModels");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 6, 1, 18, 20, 11, 8, DateTimeKind.Unspecified).AddTicks(5194), new TimeSpan(0, 5, 30, 0, 0)));
        }
    }
}
