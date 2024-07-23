using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "CustomerUser",
                type: "character varying",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "CustomerUser",
                type: "character varying",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying");

            migrationBuilder.AddColumn<string>(
                name: "Auth_Provider",
                table: "CustomerUser",
                type: "character varying",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Auth_Provider_Id",
                table: "CustomerUser",
                type: "character varying",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 10, 26, 12, 61, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 10, 26, 12, 61, DateTimeKind.Utc).AddTicks(2040));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auth_Provider",
                table: "CustomerUser");

            migrationBuilder.DropColumn(
                name: "Auth_Provider_Id",
                table: "CustomerUser");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "CustomerUser",
                type: "character varying",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "CustomerUser",
                type: "character varying",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 6, 11, 12, 138, DateTimeKind.Utc).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 6, 11, 12, 138, DateTimeKind.Utc).AddTicks(686));
        }
    }
}
