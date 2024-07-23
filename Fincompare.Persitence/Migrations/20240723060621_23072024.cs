using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class _23072024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 23, 6, 6, 20, 646, DateTimeKind.Utc).AddTicks(1787), new byte[] { 232, 111, 120, 168, 163, 202, 240, 182, 13, 142, 116, 229, 148, 42, 166, 216, 109, 193, 80, 205, 60, 3, 51, 138, 239, 37, 183, 210, 215, 227, 172, 199 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 23, 6, 6, 20, 646, DateTimeKind.Utc).AddTicks(1800), new byte[] { 232, 111, 120, 168, 163, 202, 240, 182, 13, 142, 116, 229, 148, 42, 166, 216, 109, 193, 80, 205, 60, 3, 51, 138, 239, 37, 183, 210, 215, 227, 172, 199 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 22, 11, 2, 52, 739, DateTimeKind.Utc).AddTicks(9397), new byte[] { 61, 132, 242, 242, 25, 57, 119, 156, 133, 74, 245, 180, 163, 233, 70, 85, 92, 46, 249, 136, 93, 104, 91, 66, 160, 8, 212, 6, 203, 152, 100, 152 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 22, 11, 2, 52, 739, DateTimeKind.Utc).AddTicks(9409), new byte[] { 237, 225, 80, 129, 15, 118, 17, 180, 40, 29, 128, 12, 22, 207, 96, 108, 19, 244, 48, 141, 102, 251, 62, 72, 89, 53, 5, 20, 28, 195, 174, 43 } });
        }
    }
}
