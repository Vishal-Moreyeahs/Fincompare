using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class deleteField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Asset_Description",
                table: "ActiveAsset");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 2, 12, 12, 42, 339, DateTimeKind.Utc).AddTicks(8717));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 2, 12, 12, 42, 339, DateTimeKind.Utc).AddTicks(8757));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Asset_Description",
                table: "ActiveAsset",
                type: "integer",
                maxLength: 200,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 29, 11, 28, 56, 8, DateTimeKind.Utc).AddTicks(6403));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 29, 11, 28, 56, 8, DateTimeKind.Utc).AddTicks(6412));
        }
    }
}
