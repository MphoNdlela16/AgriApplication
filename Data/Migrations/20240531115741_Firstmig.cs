using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agri_Energy_Connect.Data.Migrations
{
    /// <inheritdoc />
    public partial class Firstmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5aec4380-3fa1-4d91-b555-b48b95f36ba3", "AQAAAAIAAYagAAAAEJO8YlMBcxumDht5BwN2AYFjLwxeXeAOFMRIOYT6pFUAWQKrG7iICWZpiZBL5SOWjg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ProductionDate",
                value: new DateTime(2024, 5, 31, 13, 57, 40, 638, DateTimeKind.Local).AddTicks(2099));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ProductionDate",
                value: new DateTime(2024, 5, 31, 13, 57, 40, 638, DateTimeKind.Local).AddTicks(2114));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c18011c7-b7b6-4034-a50a-7df381d84416", "AQAAAAIAAYagAAAAED3MmImKwQzgtHmAOsqdcZPHldvnSYXWbyP81QnI5renaXlQQZaXkBwph4pqxR2LqQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ProductionDate",
                value: new DateTime(2024, 5, 31, 11, 0, 31, 62, DateTimeKind.Local).AddTicks(2188));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ProductionDate",
                value: new DateTime(2024, 5, 31, 11, 0, 31, 62, DateTimeKind.Local).AddTicks(2207));
        }
    }
}
