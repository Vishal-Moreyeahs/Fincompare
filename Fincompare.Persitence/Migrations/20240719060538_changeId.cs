using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class changeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CountryCurrency_CountryCurrencyCategory_id_fkey",
                table: "CountryCurrency");

            migrationBuilder.DropPrimaryKey(
                name: "CountryCurrencyCategory_pkey",
                table: "CountryCurrencyCategory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CountryCurrencyCategory");

            migrationBuilder.AddColumn<string>(
                name: "Country_Currency_Category_Id",
                table: "CountryCurrencyCategory",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CountryCurrencyCategory_id",
                table: "CountryCurrency",
                type: "character varying(15)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "CountryCurrencyCategory_pkey",
                table: "CountryCurrencyCategory",
                column: "Country_Currency_Category_Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 19, 6, 5, 37, 927, DateTimeKind.Utc).AddTicks(4305));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 19, 6, 5, 37, 927, DateTimeKind.Utc).AddTicks(4320));

            migrationBuilder.AddForeignKey(
                name: "CountryCurrency_CountryCurrencyCategory_id_fkey",
                table: "CountryCurrency",
                column: "CountryCurrencyCategory_id",
                principalTable: "CountryCurrencyCategory",
                principalColumn: "Country_Currency_Category_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CountryCurrency_CountryCurrencyCategory_id_fkey",
                table: "CountryCurrency");

            migrationBuilder.DropPrimaryKey(
                name: "CountryCurrencyCategory_pkey",
                table: "CountryCurrencyCategory");

            migrationBuilder.DropColumn(
                name: "Country_Currency_Category_Id",
                table: "CountryCurrencyCategory");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CountryCurrencyCategory",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "CountryCurrencyCategory_id",
                table: "CountryCurrency",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "CountryCurrencyCategory_pkey",
                table: "CountryCurrencyCategory",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 12, 19, 43, 298, DateTimeKind.Utc).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 12, 19, 43, 298, DateTimeKind.Utc).AddTicks(4724));

            migrationBuilder.AddForeignKey(
                name: "CountryCurrency_CountryCurrencyCategory_id_fkey",
                table: "CountryCurrency",
                column: "CountryCurrencyCategory_id",
                principalTable: "CountryCurrencyCategory",
                principalColumn: "Id");
        }
    }
}
