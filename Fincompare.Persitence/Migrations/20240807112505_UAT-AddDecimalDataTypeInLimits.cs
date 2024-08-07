using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class UATAddDecimalDataTypeInLimits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Send_Max_Limit",
                table: "MerchantRemitProductRate",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Send_Min_Limit",
                table: "MerchantRemitProductRate",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "receive_max_limit",
                table: "MerchantRemitProductRate",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "receive_min_limit",
                table: "MerchantRemitProductRate",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 7, 11, 25, 5, 345, DateTimeKind.Utc).AddTicks(3781));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 7, 11, 25, 5, 345, DateTimeKind.Utc).AddTicks(3796));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Send_Max_Limit",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropColumn(
                name: "Send_Min_Limit",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropColumn(
                name: "receive_max_limit",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropColumn(
                name: "receive_min_limit",
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
    }
}
