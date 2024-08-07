using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class UATDroppedMaxLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receive_Max_Limit",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropColumn(
                name: "Receive_Min_Limit",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropColumn(
                name: "Send_Max_Limit",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropColumn(
                name: "Send_Min_Limit",
                table: "MerchantRemitProductRate");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 7, 11, 18, 52, 374, DateTimeKind.Utc).AddTicks(6824));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 7, 11, 18, 52, 374, DateTimeKind.Utc).AddTicks(6841));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Receive_Max_Limit",
                table: "MerchantRemitProductRate",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Receive_Min_Limit",
                table: "MerchantRemitProductRate",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Send_Max_Limit",
                table: "MerchantRemitProductRate",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Send_Min_Limit",
                table: "MerchantRemitProductRate",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 7, 7, 9, 24, 282, DateTimeKind.Utc).AddTicks(5979));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 7, 7, 9, 24, 282, DateTimeKind.Utc).AddTicks(5994));
        }
    }
}
