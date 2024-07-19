using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class updateUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchant_Users_UserId",
                table: "Merchant");

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
                name: "Merchant_UserId_fkey",
                table: "Merchant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Merchant_UserId_fkey",
                table: "Merchant");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 12, 17, 49, 728, DateTimeKind.Utc).AddTicks(3698));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 18, 12, 17, 49, 728, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.AddForeignKey(
                name: "FK_Merchant_Users_UserId",
                table: "Merchant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
