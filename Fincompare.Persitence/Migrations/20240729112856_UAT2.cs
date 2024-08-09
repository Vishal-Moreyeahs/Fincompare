using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class UAT2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayInInstrumentId",
                table: "MerchantRemitProductFee",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Variable_Fee",
                table: "MerchantRemitProductFee",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Merchant_Type",
                table: "Merchant",
                type: "character varying",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instrument_Type",
                table: "Instrument",
                type: "character varying",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MerchantAssetPriority",
                table: "ActiveAsset",
                type: "integer",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductFee_PayInInstrumentId",
                table: "MerchantRemitProductFee",
                column: "PayInInstrumentId");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductFee_PayInInstrumentId_fkey",
                table: "MerchantRemitProductFee",
                column: "PayInInstrumentId",
                principalTable: "Instrument",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductFee_PayInInstrumentId_fkey",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropIndex(
                name: "IX_MerchantRemitProductFee_PayInInstrumentId",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropColumn(
                name: "PayInInstrumentId",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropColumn(
                name: "Variable_Fee",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropColumn(
                name: "Merchant_Type",
                table: "Merchant");

            migrationBuilder.DropColumn(
                name: "Instrument_Type",
                table: "Instrument");

            migrationBuilder.DropColumn(
                name: "MerchantAssetPriority",
                table: "ActiveAsset");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 11, 20, 4, 920, DateTimeKind.Utc).AddTicks(3694));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 11, 20, 4, 920, DateTimeKind.Utc).AddTicks(3707));
        }
    }
}
