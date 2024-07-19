using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class Mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IsCustomer", "IsMerchant", "IsVendor" },
                values: new object[] { true, true, true });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsVendor",
                value: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 19, 12, 5, 24, 639, DateTimeKind.Utc).AddTicks(7620), "carl.unni@fincomapare.com", "Carl", "Unni", new byte[] { 14, 123, 189, 169, 164, 231, 101, 68, 214, 108, 132, 94, 188, 171, 111, 216, 226, 180, 76, 13, 98, 25, 72, 121, 55, 96, 217, 148, 113, 21, 11, 2 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 19, 12, 5, 24, 639, DateTimeKind.Utc).AddTicks(7634), "sailesh.pillai@fincompare.com", "Sailesh", "Pillai", new byte[] { 197, 17, 217, 42, 199, 33, 186, 20, 193, 207, 238, 43, 135, 20, 53, 57, 69, 120, 54, 121, 11, 52, 23, 255, 6, 78, 9, 184, 82, 213, 173, 238 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IsCustomer", "IsMerchant", "IsVendor" },
                values: new object[] { false, false, false });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsVendor",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 19, 11, 24, 1, 981, DateTimeKind.Utc).AddTicks(7371), "aarya.garg@moreyeahs.com", "Aarya", "Garg", new byte[] { 174, 37, 40, 78, 61, 77, 69, 19, 80, 223, 202, 222, 187, 5, 98, 239, 4, 250, 125, 131, 152, 224, 149, 111, 155, 248, 50, 77, 228, 16, 121, 219 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 19, 11, 24, 1, 981, DateTimeKind.Utc).AddTicks(7384), "vishal.pawar@moreyeahs.com", "Vishal", "Pawar", new byte[] { 191, 77, 99, 205, 83, 55, 86, 113, 215, 255, 103, 106, 200, 175, 114, 38, 28, 185, 72, 18, 181, 201, 106, 152, 78, 60, 123, 188, 38, 184, 208, 240 } });
        }
    }
}
