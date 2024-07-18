using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class userid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Merchant_UserId_fkey",
                table: "Merchant");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 20, 10, 351, DateTimeKind.Utc).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 20, 10, 351, DateTimeKind.Utc).AddTicks(8557));

            migrationBuilder.AddForeignKey(
                name: "Merchant_UserId_fkey",
                table: "Merchant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Merchant_UserId_fkey",
                table: "Merchant");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 15, 29, 325, DateTimeKind.Utc).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 15, 29, 325, DateTimeKind.Utc).AddTicks(7361));

            migrationBuilder.AddForeignKey(
                name: "Merchant_UserId_fkey",
                table: "Merchant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
