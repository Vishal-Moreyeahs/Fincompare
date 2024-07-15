using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CountryCurrency_Currency_Id_fkey",
                table: "CountryCurrency");

            migrationBuilder.DropForeignKey(
                name: "CustomerRateSubscription_Receive_Cur_fkey",
                table: "CustomerRateSubscription");

            migrationBuilder.DropForeignKey(
                name: "CustomerRateSubscription_Send_Cur_fkey",
                table: "CustomerRateSubscription");

            migrationBuilder.DropForeignKey(
                name: "MarketRate_Currency_Id_fkey1",
                table: "MarketRate");

            migrationBuilder.DropForeignKey(
                name: "MarketRate_Send_Cur_fkey",
                table: "MarketRate");

            migrationBuilder.DropForeignKey(
                name: "MerchantProduct_Receive_Currency_Id_fkey",
                table: "MerchantProduct");

            migrationBuilder.DropForeignKey(
                name: "MerchantProduct_Send_Currency_Id_fkey",
                table: "MerchantProduct");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductFee_Fees_Cur_fkey",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductFee_Receive_Currency_fkey",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductFee_Send_Currency_fkey",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductRate_Receive_Cur_fkey",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductRate_Send_Cur_fkey",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropPrimaryKey(
                name: "Currency_pkey",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_CountryCurrency_Currency_Id",
                table: "CountryCurrency");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "Currency_Id",
                table: "CountryCurrency");

            migrationBuilder.AlterColumn<string>(
                name: "Send_Cur",
                table: "MerchantRemitProductRate",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Receive_Cur",
                table: "MerchantRemitProductRate",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Send_Currency",
                table: "MerchantRemitProductFee",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Receive_Currency",
                table: "MerchantRemitProductFee",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Fees_Cur",
                table: "MerchantRemitProductFee",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Send_Currency_Id",
                table: "MerchantProduct",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Receive_Currency_Id",
                table: "MerchantProduct",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Send_Cur",
                table: "MarketRate",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Receive_Cur",
                table: "MarketRate",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Send_Cur",
                table: "CustomerRateSubscription",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Receive_Cur",
                table: "CustomerRateSubscription",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyIso",
                table: "Currency",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency_3_iso",
                table: "CountryCurrency",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "Currency_pkey",
                table: "Currency",
                column: "CurrencyIso");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 14, 16, 30, 59, 437, DateTimeKind.Utc).AddTicks(8775));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 14, 16, 30, 59, 437, DateTimeKind.Utc).AddTicks(8795));

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrency_Currency_3_iso",
                table: "CountryCurrency",
                column: "Currency_3_iso");

            migrationBuilder.AddForeignKey(
                name: "CountryCurrency_Currency_3_iso_fkey",
                table: "CountryCurrency",
                column: "Currency_3_iso",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "CustomerRateSubscription_Receive_Cur_fkey",
                table: "CustomerRateSubscription",
                column: "Receive_Cur",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "CustomerRateSubscription_Send_Cur_fkey",
                table: "CustomerRateSubscription",
                column: "Send_Cur",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MarketRate_Currency_Id_fkey1",
                table: "MarketRate",
                column: "Receive_Cur",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MarketRate_Send_Cur_fkey",
                table: "MarketRate",
                column: "Send_Cur",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MerchantProduct_Receive_Currency_Id_fkey",
                table: "MerchantProduct",
                column: "Receive_Currency_Id",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MerchantProduct_Send_Currency_Id_fkey",
                table: "MerchantProduct",
                column: "Send_Currency_Id",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductFee_Fees_Cur_fkey",
                table: "MerchantRemitProductFee",
                column: "Fees_Cur",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductFee_Receive_Currency_fkey",
                table: "MerchantRemitProductFee",
                column: "Receive_Currency",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductFee_Send_Currency_fkey",
                table: "MerchantRemitProductFee",
                column: "Send_Currency",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductRate_Receive_Cur_fkey",
                table: "MerchantRemitProductRate",
                column: "Receive_Cur",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductRate_Send_Cur_fkey",
                table: "MerchantRemitProductRate",
                column: "Send_Cur",
                principalTable: "Currency",
                principalColumn: "CurrencyIso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CountryCurrency_Currency_3_iso_fkey",
                table: "CountryCurrency");

            migrationBuilder.DropForeignKey(
                name: "CustomerRateSubscription_Receive_Cur_fkey",
                table: "CustomerRateSubscription");

            migrationBuilder.DropForeignKey(
                name: "CustomerRateSubscription_Send_Cur_fkey",
                table: "CustomerRateSubscription");

            migrationBuilder.DropForeignKey(
                name: "MarketRate_Currency_Id_fkey1",
                table: "MarketRate");

            migrationBuilder.DropForeignKey(
                name: "MarketRate_Send_Cur_fkey",
                table: "MarketRate");

            migrationBuilder.DropForeignKey(
                name: "MerchantProduct_Receive_Currency_Id_fkey",
                table: "MerchantProduct");

            migrationBuilder.DropForeignKey(
                name: "MerchantProduct_Send_Currency_Id_fkey",
                table: "MerchantProduct");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductFee_Fees_Cur_fkey",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductFee_Receive_Currency_fkey",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductFee_Send_Currency_fkey",
                table: "MerchantRemitProductFee");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductRate_Receive_Cur_fkey",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropForeignKey(
                name: "MerchantRemitProductRate_Send_Cur_fkey",
                table: "MerchantRemitProductRate");

            migrationBuilder.DropPrimaryKey(
                name: "Currency_pkey",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_CountryCurrency_Currency_3_iso",
                table: "CountryCurrency");

            migrationBuilder.DropColumn(
                name: "Currency_3_iso",
                table: "CountryCurrency");

            migrationBuilder.AlterColumn<int>(
                name: "Send_Cur",
                table: "MerchantRemitProductRate",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Receive_Cur",
                table: "MerchantRemitProductRate",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Send_Currency",
                table: "MerchantRemitProductFee",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Receive_Currency",
                table: "MerchantRemitProductFee",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Fees_Cur",
                table: "MerchantRemitProductFee",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Send_Currency_Id",
                table: "MerchantProduct",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Receive_Currency_Id",
                table: "MerchantProduct",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Send_Cur",
                table: "MarketRate",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Receive_Cur",
                table: "MarketRate",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Send_Cur",
                table: "CustomerRateSubscription",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Receive_Cur",
                table: "CustomerRateSubscription",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyIso",
                table: "Currency",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Currency",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Currency_Id",
                table: "CountryCurrency",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "Currency_pkey",
                table: "Currency",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 11, 18, 15, 34, 249, DateTimeKind.Utc).AddTicks(3515));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 11, 18, 15, 34, 249, DateTimeKind.Utc).AddTicks(3535));

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrency_Currency_Id",
                table: "CountryCurrency",
                column: "Currency_Id");

            migrationBuilder.AddForeignKey(
                name: "CountryCurrency_Currency_Id_fkey",
                table: "CountryCurrency",
                column: "Currency_Id",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "CustomerRateSubscription_Receive_Cur_fkey",
                table: "CustomerRateSubscription",
                column: "Receive_Cur",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "CustomerRateSubscription_Send_Cur_fkey",
                table: "CustomerRateSubscription",
                column: "Send_Cur",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MarketRate_Currency_Id_fkey1",
                table: "MarketRate",
                column: "Receive_Cur",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MarketRate_Send_Cur_fkey",
                table: "MarketRate",
                column: "Send_Cur",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MerchantProduct_Receive_Currency_Id_fkey",
                table: "MerchantProduct",
                column: "Receive_Currency_Id",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MerchantProduct_Send_Currency_Id_fkey",
                table: "MerchantProduct",
                column: "Send_Currency_Id",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductFee_Fees_Cur_fkey",
                table: "MerchantRemitProductFee",
                column: "Fees_Cur",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductFee_Receive_Currency_fkey",
                table: "MerchantRemitProductFee",
                column: "Receive_Currency",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductFee_Send_Currency_fkey",
                table: "MerchantRemitProductFee",
                column: "Send_Currency",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductRate_Receive_Cur_fkey",
                table: "MerchantRemitProductRate",
                column: "Receive_Cur",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MerchantRemitProductRate_Send_Cur_fkey",
                table: "MerchantRemitProductRate",
                column: "Send_Cur",
                principalTable: "Currency",
                principalColumn: "Id");
        }
    }
}
