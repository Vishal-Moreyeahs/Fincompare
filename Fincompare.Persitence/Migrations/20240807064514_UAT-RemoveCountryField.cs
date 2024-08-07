using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class UATRemoveCountryField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Instrument_Country_3_iso_fkey",
                table: "Instrument");

            migrationBuilder.DropForeignKey(
                name: "Product_Country_3_iso_fkey",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "ServiceCategory_Country_3_iso_fkey",
                table: "ServiceCategory");

            migrationBuilder.RenameColumn(
                name: "Country_3_iso",
                table: "ServiceCategory",
                newName: "Country3IsoNavigationCountry3Iso");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceCategory_Country_3_iso",
                table: "ServiceCategory",
                newName: "IX_ServiceCategory_Country3IsoNavigationCountry3Iso");

            migrationBuilder.RenameColumn(
                name: "Country_3_iso",
                table: "Product",
                newName: "Country3IsoNavigationCountry3Iso");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Country_3_iso",
                table: "Product",
                newName: "IX_Product_Country3IsoNavigationCountry3Iso");

            migrationBuilder.RenameColumn(
                name: "Country_3_iso",
                table: "Instrument",
                newName: "Country3IsoNavigationCountry3Iso");

            migrationBuilder.RenameIndex(
                name: "IX_Instrument_Country_3_iso",
                table: "Instrument",
                newName: "IX_Instrument_Country3IsoNavigationCountry3Iso");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 7, 6, 45, 14, 65, DateTimeKind.Utc).AddTicks(2046));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 7, 6, 45, 14, 65, DateTimeKind.Utc).AddTicks(2061));

            migrationBuilder.AddForeignKey(
                name: "FK_Instrument_Country_Country3IsoNavigationCountry3Iso",
                table: "Instrument",
                column: "Country3IsoNavigationCountry3Iso",
                principalTable: "Country",
                principalColumn: "Country_3_iso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Country_Country3IsoNavigationCountry3Iso",
                table: "Product",
                column: "Country3IsoNavigationCountry3Iso",
                principalTable: "Country",
                principalColumn: "Country_3_iso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCategory_Country_Country3IsoNavigationCountry3Iso",
                table: "ServiceCategory",
                column: "Country3IsoNavigationCountry3Iso",
                principalTable: "Country",
                principalColumn: "Country_3_iso",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instrument_Country_Country3IsoNavigationCountry3Iso",
                table: "Instrument");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Country_Country3IsoNavigationCountry3Iso",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCategory_Country_Country3IsoNavigationCountry3Iso",
                table: "ServiceCategory");

            migrationBuilder.RenameColumn(
                name: "Country3IsoNavigationCountry3Iso",
                table: "ServiceCategory",
                newName: "Country_3_iso");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceCategory_Country3IsoNavigationCountry3Iso",
                table: "ServiceCategory",
                newName: "IX_ServiceCategory_Country_3_iso");

            migrationBuilder.RenameColumn(
                name: "Country3IsoNavigationCountry3Iso",
                table: "Product",
                newName: "Country_3_iso");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Country3IsoNavigationCountry3Iso",
                table: "Product",
                newName: "IX_Product_Country_3_iso");

            migrationBuilder.RenameColumn(
                name: "Country3IsoNavigationCountry3Iso",
                table: "Instrument",
                newName: "Country_3_iso");

            migrationBuilder.RenameIndex(
                name: "IX_Instrument_Country3IsoNavigationCountry3Iso",
                table: "Instrument",
                newName: "IX_Instrument_Country_3_iso");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 13, 5, 58, 902, DateTimeKind.Utc).AddTicks(5525));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 13, 5, 58, 902, DateTimeKind.Utc).AddTicks(5535));

            migrationBuilder.AddForeignKey(
                name: "Instrument_Country_3_iso_fkey",
                table: "Instrument",
                column: "Country_3_iso",
                principalTable: "Country",
                principalColumn: "Country_3_iso");

            migrationBuilder.AddForeignKey(
                name: "Product_Country_3_iso_fkey",
                table: "Product",
                column: "Country_3_iso",
                principalTable: "Country",
                principalColumn: "Country_3_iso");

            migrationBuilder.AddForeignKey(
                name: "ServiceCategory_Country_3_iso_fkey",
                table: "ServiceCategory",
                column: "Country_3_iso",
                principalTable: "Country",
                principalColumn: "Country_3_iso");
        }
    }
}
