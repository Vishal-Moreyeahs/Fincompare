using System;
using Microsoft.EntityFrameworkCore.Migrations;

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Merchant",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 35, 22, 952, DateTimeKind.Utc).AddTicks(3159));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 35, 22, 952, DateTimeKind.Utc).AddTicks(3182));

            migrationBuilder.AddForeignKey(
                name: "FK_Merchant_Users_UserId",
                table: "Merchant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchant_Users_UserId",
                table: "Merchant");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Merchant",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 33, 42, 16, DateTimeKind.Utc).AddTicks(1965));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 11, 33, 42, 16, DateTimeKind.Utc).AddTicks(1983));

            migrationBuilder.AddForeignKey(
                name: "Merchant_UserId_fkey",
                table: "Merchant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
