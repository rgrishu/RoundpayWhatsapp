using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class seedrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "d56d9543-b61e-4c7a-a611-ebc073d5da71", "Admin", "ADMIN" },
                    { 2, "23e0f213-c8fa-4d53-bf9b-5c6220d2e3ba", "Seller", "SELLER" },
                    { 3, "da576a70-2276-4872-a496-6765a07534e6", "ReSeller", "RESELLER" },
                    { 4, "b52abe85-ca0e-44ae-a4d0-fd1b315576ee", "WhiteLabel", "WHITELEABEL" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
