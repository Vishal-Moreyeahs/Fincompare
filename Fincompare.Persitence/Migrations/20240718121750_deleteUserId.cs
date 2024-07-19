﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class deleteUserId : Migration
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
                value: new DateTime(2024, 7, 18, 12, 17, 49, 728, DateTimeKind.Utc).AddTicks(3698));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 12, 17, 49, 728, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.AddForeignKey(
                name: "FK_Merchant_Users_UserId",
                table: "Merchant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchant_Users_UserId",
                table: "Merchant");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 39, 6, 816, DateTimeKind.Utc).AddTicks(9169));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 39, 6, 816, DateTimeKind.Utc).AddTicks(9193));

            migrationBuilder.AddForeignKey(
                name: "Merchant_UserId_fkey",
                table: "Merchant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
