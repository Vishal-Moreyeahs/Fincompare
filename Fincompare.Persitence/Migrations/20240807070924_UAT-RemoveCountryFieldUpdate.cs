using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class UATRemoveCountryFieldUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_ServiceCategory_Country3IsoNavigationCountry3Iso",
                table: "ServiceCategory");

            migrationBuilder.DropIndex(
                name: "IX_Product_Country3IsoNavigationCountry3Iso",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Instrument_Country3IsoNavigationCountry3Iso",
                table: "Instrument");

            migrationBuilder.DropColumn(
                name: "Country3IsoNavigationCountry3Iso",
                table: "ServiceCategory");

            migrationBuilder.DropColumn(
                name: "Country3IsoNavigationCountry3Iso",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Country3IsoNavigationCountry3Iso",
                table: "Instrument");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country3IsoNavigationCountry3Iso",
                table: "ServiceCategory",
                type: "character varying",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country3IsoNavigationCountry3Iso",
                table: "Product",
                type: "character varying",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country3IsoNavigationCountry3Iso",
                table: "Instrument",
                type: "character varying",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_Country3IsoNavigationCountry3Iso",
                table: "ServiceCategory",
                column: "Country3IsoNavigationCountry3Iso");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Country3IsoNavigationCountry3Iso",
                table: "Product",
                column: "Country3IsoNavigationCountry3Iso");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_Country3IsoNavigationCountry3Iso",
                table: "Instrument",
                column: "Country3IsoNavigationCountry3Iso");

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
    }
}
