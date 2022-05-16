using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsapp.Migrations
{
    public partial class seedapitype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MasterApiType",
                columns: new[] { "Id", "ApiTypeName", "CreatedDate", "IPAddress", "ModifiedDate" },
                values: new object[] { 1L, "Whatsapp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "MasterApiType",
                columns: new[] { "Id", "ApiTypeName", "CreatedDate", "IPAddress", "ModifiedDate" },
                values: new object[] { 2L, "Email", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "MasterApiType",
                columns: new[] { "Id", "ApiTypeName", "CreatedDate", "IPAddress", "ModifiedDate" },
                values: new object[] { 3L, "SMS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MasterApiType",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "MasterApiType",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "MasterApiType",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
