using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class updateMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 19, 12, 54, 22, 647, DateTimeKind.Utc).AddTicks(8361));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 19, 12, 54, 22, 647, DateTimeKind.Utc).AddTicks(8378), new byte[] { 237, 225, 80, 129, 15, 118, 17, 180, 40, 29, 128, 12, 22, 207, 96, 108, 19, 244, 48, 141, 102, 251, 62, 72, 89, 53, 5, 20, 28, 195, 174, 43 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 19, 12, 52, 29, 111, DateTimeKind.Utc).AddTicks(8381));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 19, 12, 52, 29, 111, DateTimeKind.Utc).AddTicks(8392), new byte[] { 197, 17, 217, 42, 199, 33, 186, 20, 193, 207, 238, 43, 135, 20, 53, 57, 69, 120, 54, 121, 11, 52, 23, 255, 6, 78, 9, 184, 82, 213, 173, 238 } });
        }
    }
}
