using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 19, 12, 52, 29, 111, DateTimeKind.Utc).AddTicks(8381), "carl.unni@fincompare.com", new byte[] { 61, 132, 242, 242, 25, 57, 119, 156, 133, 74, 245, 180, 163, 233, 70, 85, 92, 46, 249, 136, 93, 104, 91, 66, 160, 8, 212, 6, 203, 152, 100, 152 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 19, 12, 52, 29, 111, DateTimeKind.Utc).AddTicks(8392));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 19, 12, 5, 24, 639, DateTimeKind.Utc).AddTicks(7620), "carl.unni@fincomapare.com", new byte[] { 14, 123, 189, 169, 164, 231, 101, 68, 214, 108, 132, 94, 188, 171, 111, 216, 226, 180, 76, 13, 98, 25, 72, 121, 55, 96, 217, 148, 113, 21, 11, 2 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 19, 12, 5, 24, 639, DateTimeKind.Utc).AddTicks(7634));
        }
    }
}
