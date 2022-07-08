using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class AddedWABAModelAndSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WABAsModels");

            migrationBuilder.CreateTable(
                name: "WABAsProviders",
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
                    table.PrimaryKey("PK_WABAsProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WABAsNumbers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    WABAsId = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    Protocol = table.Column<int>(nullable: false),
                    CallbackURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WABAsNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WABAsNumbers_WABAsProviders_WABAsId",
                        column: x => x.WABAsId,
                        principalTable: "WABAsProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 6, 28, 11, 53, 22, 110, DateTimeKind.Unspecified).AddTicks(6062), new TimeSpan(0, 5, 30, 0, 0)));

            migrationBuilder.InsertData(
                table: "WABAsProviders",
                columns: new[] { "Id", "APICode", "APIId", "IsActive", "ProviderName" },
                values: new object[] { 1, "RNDPAY", 1, true, "ROUNDPAY1" });

            migrationBuilder.CreateIndex(
                name: "IX_WABAsNumbers_WABAsId",
                table: "WABAsNumbers",
                column: "WABAsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WABAsNumbers");

            migrationBuilder.DropTable(
                name: "WABAsProviders");

            migrationBuilder.CreateTable(
                name: "WABAsModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APICode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APIId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
