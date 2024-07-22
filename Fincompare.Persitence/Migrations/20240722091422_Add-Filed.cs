using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class AddFiled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 22, 9, 14, 22, 594, DateTimeKind.Utc).AddTicks(7573));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 22, 9, 14, 22, 594, DateTimeKind.Utc).AddTicks(7584));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 22, 9, 13, 29, 414, DateTimeKind.Utc).AddTicks(3804));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 22, 9, 13, 29, 414, DateTimeKind.Utc).AddTicks(3813));
        }
    }
}
