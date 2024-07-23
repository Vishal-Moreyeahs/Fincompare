using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fincompare.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class UAT1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Country_2_iso = table.Column<string>(type: "character varying", nullable: true),
                    Country_Name = table.Column<string>(type: "character varying", maxLength: 40, nullable: false),
                    Web_link = table.Column<string>(type: "character varying", maxLength: 150, nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Country_pkey", x => x.Country_3_iso);
                });

            migrationBuilder.CreateTable(
                name: "CountryCurrencyCategory",
                columns: table => new
                {
                    Country_Currency_Category_Id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Definition = table.Column<string>(type: "character varying", maxLength: 35, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CountryCurrencyCategory_pkey", x => x.Country_Currency_Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Coupon_Name = table.Column<string>(type: "character varying", nullable: false),
                    Coupon_Format = table.Column<string>(type: "character varying", maxLength: 35, nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Coupon_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyIso = table.Column<string>(type: "text", nullable: false),
                    Currency_Name = table.Column<string>(type: "character varying", maxLength: 11, nullable: false),
                    Decimal = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Volatility_Range = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Currency_pkey", x => x.CurrencyIso);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PermissionName = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsMerchant = table.Column<bool>(type: "boolean", nullable: false),
                    IsVendor = table.Column<bool>(type: "boolean", nullable: false),
                    IsCustomer = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusCode = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetsMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Asset_Name = table.Column<string>(type: "character varying", maxLength: 150, nullable: false),
                    Asset_Description = table.Column<string>(type: "character varying", maxLength: 150, nullable: false),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AssetsMaster_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "AssetsMaster_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                });

            migrationBuilder.CreateTable(
                name: "GroupMerchant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Group_Merchant_Name = table.Column<string>(type: "character varying", maxLength: 55, nullable: false),
                    Group_Merchant_ShortName = table.Column<string>(type: "character varying", maxLength: 25, nullable: false),
                    Group_Ph1 = table.Column<string>(type: "character varying", maxLength: 15, nullable: false),
                    Group_Ph2 = table.Column<string>(type: "character varying", maxLength: 15, nullable: true),
                    Group_Em1 = table.Column<string>(type: "character varying", maxLength: 50, nullable: false),
                    Group_Em2 = table.Column<string>(type: "character varying", maxLength: 50, nullable: true),
                    Group_CSPh = table.Column<string>(type: "character varying", maxLength: 15, nullable: false),
                    Group_CSEm = table.Column<string>(type: "character varying", maxLength: 50, nullable: false),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GroupMerchant_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "GroupMerchant_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Instrument_Name = table.Column<string>(type: "character varying", maxLength: 25, nullable: false),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Instrument_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Instrument_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                });

            migrationBuilder.CreateTable(
                name: "RateCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Rate_Card = table.Column<string>(type: "character varying", maxLength: 7, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RateCard_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "RateCard_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Serv_Category_Name = table.Column<string>(type: "character varying", maxLength: 25, nullable: false),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ServiceCategory_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "ServiceCategory_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    State_Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("State_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "State_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                });

            migrationBuilder.CreateTable(
                name: "CountryCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Currency_3_iso = table.Column<string>(type: "text", nullable: true),
                    IsPrimary_Cur = table.Column<bool>(type: "boolean", nullable: false),
                    CountryCurrencyCategory_id = table.Column<string>(type: "character varying(15)", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CountryCurrency_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CountryCurrency_CountryCurrencyCategory_id_fkey",
                        column: x => x.CountryCurrencyCategory_id,
                        principalTable: "CountryCurrencyCategory",
                        principalColumn: "Country_Currency_Category_Id");
                    table.ForeignKey(
                        name: "CountryCurrency_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "CountryCurrency_Currency_3_iso_fkey",
                        column: x => x.Currency_3_iso,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                });

            migrationBuilder.CreateTable(
                name: "MarketRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Send_Cur = table.Column<string>(type: "text", nullable: false),
                    Receive_Cur = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Rate_Source = table.Column<string>(type: "character varying", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MarketRate_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "MarketRate_Currency_Id_fkey1",
                        column: x => x.Receive_Cur,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                    table.ForeignKey(
                        name: "MarketRate_Send_Cur_fkey",
                        column: x => x.Send_Cur,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "User_StatusId_fkey",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Product_Name = table.Column<string>(type: "character varying", maxLength: 25, nullable: false),
                    Product_Description = table.Column<string>(type: "character varying", maxLength: 75, nullable: false),
                    ServiceCategory_Id = table.Column<int>(type: "integer", nullable: false),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Product_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "Product_ServiceCategory_Id_fkey",
                        column: x => x.ServiceCategory_Id,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City_Name = table.Column<string>(type: "character varying", maxLength: 100, nullable: false),
                    State_id = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("City_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "City_State_id_fkey",
                        column: x => x.State_id,
                        principalTable: "State",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerName = table.Column<string>(type: "character varying", maxLength: 40, nullable: false),
                    Email_Id = table.Column<string>(type: "character varying", maxLength: 35, nullable: false),
                    Phone = table.Column<string>(type: "character varying", maxLength: 16, nullable: false),
                    Address = table.Column<string>(type: "character varying", maxLength: 35, nullable: false),
                    State = table.Column<string>(type: "character varying", nullable: true),
                    City = table.Column<string>(type: "character varying", nullable: true),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Rate_Subscription = table.Column<bool>(type: "boolean", nullable: false),
                    Promo_Subscription = table.Column<bool>(type: "boolean", nullable: false),
                    Auth_Provider = table.Column<string>(type: "character varying", maxLength: 35, nullable: true),
                    Auth_Provider_Id = table.Column<string>(type: "character varying", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomerUser_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomerUser_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "CustomerUser_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Merchant_Name = table.Column<string>(type: "character varying", maxLength: 75, nullable: false),
                    Merchant_ShortName = table.Column<string>(type: "character varying", maxLength: 25, nullable: false),
                    GroupMerchant_id = table.Column<int>(type: "integer", nullable: false),
                    Merchant_CSPh = table.Column<string>(type: "character varying", maxLength: 15, nullable: false),
                    Merchant_CSEm = table.Column<string>(type: "character varying", maxLength: 50, nullable: false),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Affiliate_Id = table.Column<string>(type: "character varying", maxLength: 36, nullable: true),
                    Merchant_Ph1 = table.Column<string>(type: "character varying", maxLength: 15, nullable: false),
                    Merchant_Ph2 = table.Column<string>(type: "character varying", maxLength: 15, nullable: true),
                    Merchant_EM1 = table.Column<string>(type: "character varying", maxLength: 50, nullable: false),
                    Merchant_EM2 = table.Column<string>(type: "character varying", maxLength: 50, nullable: true),
                    Routing_Parameters = table.Column<string>(type: "character varying", maxLength: 200, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Web_Url = table.Column<string>(type: "character varying", maxLength: 300, nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Merchant_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Merchant_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "Merchant_GroupMerchant_id_fkey",
                        column: x => x.GroupMerchant_id,
                        principalTable: "GroupMerchant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Merchant_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRateSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerUser_Id = table.Column<int>(type: "integer", nullable: false),
                    Send_Cur = table.Column<string>(type: "text", nullable: false),
                    Receive_Cur = table.Column<string>(type: "text", nullable: false),
                    Wish_Rate = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomerRateSubscription_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomerRateSubscription_CustomerUser_Id_fkey",
                        column: x => x.CustomerUser_Id,
                        principalTable: "CustomerUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomerRateSubscription_Receive_Cur_fkey",
                        column: x => x.Receive_Cur,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                    table.ForeignKey(
                        name: "CustomerRateSubscription_Send_Cur_fkey",
                        column: x => x.Send_Cur,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                });

            migrationBuilder.CreateTable(
                name: "ActiveAsset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetsMaster_Id = table.Column<int>(type: "integer", nullable: false),
                    Asset_Description = table.Column<int>(type: "integer", maxLength: 200, nullable: false),
                    Merchant_Id = table.Column<int>(type: "integer", nullable: false),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Asset_Merchant_Url = table.Column<string>(type: "character varying", maxLength: 200, nullable: false),
                    ServiceCategory_Id = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Date_Active = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date_Validity = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ActiveAsset_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "ActiveAsset_AssetsMaster_Id_fkey",
                        column: x => x.AssetsMaster_Id,
                        principalTable: "AssetsMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ActiveAsset_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "ActiveAsset_Merchant_Id_fkey",
                        column: x => x.Merchant_Id,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ActiveAsset_ServiceCategory_Id_fkey",
                        column: x => x.ServiceCategory_Id,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClickLead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerUser_Id = table.Column<int>(type: "integer", nullable: true),
                    Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Merchant_Id = table.Column<int>(type: "integer", nullable: false),
                    Routing_Paramters = table.Column<string>(type: "character varying", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ClickLead_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "ClickLead_Country_3_iso_fkey",
                        column: x => x.Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "ClickLead_CustomerUser_Id_fkey",
                        column: x => x.CustomerUser_Id,
                        principalTable: "CustomerUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ClickLead_Merchant_Id_fkey",
                        column: x => x.Merchant_Id,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Merchant_Id = table.Column<int>(type: "integer", nullable: false),
                    Review = table.Column<string>(type: "character varying", maxLength: 400, nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomerReview_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomerReview_Merchant_Id_fkey",
                        column: x => x.Merchant_Id,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceCategory_Id = table.Column<int>(type: "integer", nullable: false),
                    Instrument_Id = table.Column<int>(type: "integer", nullable: false),
                    Product_Id = table.Column<int>(type: "integer", nullable: false),
                    Merchant_Id = table.Column<int>(type: "integer", nullable: false),
                    Send_Country_3_iso = table.Column<string>(type: "character varying", nullable: true),
                    Receive_Country_3_iso = table.Column<string>(type: "character varying", nullable: true),
                    Send_Currency_Id = table.Column<string>(type: "text", nullable: false),
                    Receive_Currency_Id = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Service_Levels = table.Column<string>(type: "character varying", maxLength: 6, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MerchantProduct_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "MerchantProduct_Instrument_Id_fkey",
                        column: x => x.Instrument_Id,
                        principalTable: "Instrument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantProduct_Merchant_Id_fkey",
                        column: x => x.Merchant_Id,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantProduct_Product_Id_fkey",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantProduct_Receive_Country_3_iso_fkey",
                        column: x => x.Receive_Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "MerchantProduct_Receive_Currency_Id_fkey",
                        column: x => x.Receive_Currency_Id,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                    table.ForeignKey(
                        name: "MerchantProduct_Send_Country_3_iso_fkey",
                        column: x => x.Send_Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "MerchantProduct_Send_Currency_Id_fkey",
                        column: x => x.Send_Currency_Id,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                    table.ForeignKey(
                        name: "MerchantProduct_ServiceCategory_Id_fkey",
                        column: x => x.ServiceCategory_Id,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantCampaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Campaign_Code = table.Column<string>(type: "character varying", maxLength: 50, nullable: false),
                    Campaign_Description = table.Column<string>(type: "character varying", maxLength: 300, nullable: false),
                    Merchant_Id = table.Column<int>(type: "integer", nullable: false),
                    ServiceCategory_Id = table.Column<int>(type: "integer", nullable: false),
                    MerchantProduct_Id = table.Column<int>(type: "integer", nullable: true),
                    Send_Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Receive_Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Date_Active = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date_Validity = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MerchantCampaign_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "MerchantCampaign_MerchantProduct_Id_fkey",
                        column: x => x.MerchantProduct_Id,
                        principalTable: "MerchantProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantCampaign_Merchant_Id_fkey",
                        column: x => x.Merchant_Id,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantCampaign_Receive_Country_3_iso_fkey",
                        column: x => x.Receive_Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "MerchantCampaign_Send_Country_3_iso_fkey",
                        column: x => x.Send_Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "MerchantCampaign_ServiceCategory_Id_fkey",
                        column: x => x.ServiceCategory_Id,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantProductCoupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Coupon_Id = table.Column<int>(type: "integer", nullable: false),
                    MerchantProduct_Id = table.Column<int>(type: "integer", nullable: true),
                    Coupon_Code = table.Column<string>(type: "character varying", maxLength: 10, nullable: false),
                    IsMultiple = table.Column<bool>(type: "boolean", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    MerchantId = table.Column<int>(type: "integer", nullable: true),
                    Merchant_Coupon_Batch = table.Column<string>(type: "character varying", maxLength: 36, nullable: true),
                    ValidityFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidityTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MerchantProductCoupon_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "MerchantProductCoupon_Coupon_Id_fkey",
                        column: x => x.Coupon_Id,
                        principalTable: "Coupon",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantProductCoupon_MerchantProduct_Id_fkey",
                        column: x => x.MerchantProduct_Id,
                        principalTable: "MerchantProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantProductCoupon_Merchant_Id_fkey",
                        column: x => x.MerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantRemitProductFee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Merchant_Id = table.Column<int>(type: "integer", nullable: false),
                    Fees_Name = table.Column<string>(type: "character varying", maxLength: 35, nullable: false),
                    Fees_Cur = table.Column<string>(type: "text", nullable: false),
                    Fees = table.Column<double>(type: "double precision", nullable: false),
                    Promo_Fees = table.Column<double>(type: "double precision", nullable: true),
                    MerchantProduct_Id = table.Column<int>(type: "integer", nullable: true),
                    Send_Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Receive_Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Send_Currency = table.Column<string>(type: "text", nullable: false),
                    Receive_Currency = table.Column<string>(type: "text", nullable: false),
                    Send_Min_Limit = table.Column<double>(type: "double precision", nullable: false),
                    Send_Max_Limit = table.Column<double>(type: "double precision", nullable: false),
                    Receive_Min_Limit = table.Column<double>(type: "double precision", nullable: false),
                    Receive_Max_Limit = table.Column<double>(type: "double precision", nullable: false),
                    ValidityExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MerchantRemitProductFee_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "MerchantRemitProductFee_Fees_Cur_fkey",
                        column: x => x.Fees_Cur,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                    table.ForeignKey(
                        name: "MerchantRemitProductFee_MerchantProduct_Id_fkey",
                        column: x => x.MerchantProduct_Id,
                        principalTable: "MerchantProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantRemitProductFee_Merchant_Id_fkey",
                        column: x => x.Merchant_Id,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantRemitProductFee_Receive_Country_3_iso_fkey",
                        column: x => x.Receive_Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "MerchantRemitProductFee_Receive_Currency_fkey",
                        column: x => x.Receive_Currency,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                    table.ForeignKey(
                        name: "MerchantRemitProductFee_Send_Country_3_iso_fkey",
                        column: x => x.Send_Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "MerchantRemitProductFee_Send_Currency_fkey",
                        column: x => x.Send_Currency,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                });

            migrationBuilder.CreateTable(
                name: "MerchantRemitProductRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Merchant_Rate_Ref = table.Column<string>(type: "character varying", maxLength: 36, nullable: false),
                    Merchant_Id = table.Column<int>(type: "integer", nullable: false),
                    MerchantProduct_Id = table.Column<int>(type: "integer", nullable: true),
                    Send_Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Receive_Country_3_iso = table.Column<string>(type: "character varying", nullable: false),
                    Send_Cur = table.Column<string>(type: "text", nullable: false),
                    Receive_Cur = table.Column<string>(type: "text", nullable: false),
                    Send_Min_Limit = table.Column<int>(type: "integer", nullable: false),
                    Send_Max_Limit = table.Column<int>(type: "integer", nullable: false),
                    Receive_Min_Limit = table.Column<int>(type: "integer", nullable: false),
                    Receive_Max_Limit = table.Column<int>(type: "integer", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    Promo_Rate = table.Column<double>(type: "double precision", nullable: false),
                    Validity_Expiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MerchantRemitProductRate_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "MerchantRemitProductRate_MerchantProduct_Id_fkey",
                        column: x => x.MerchantProduct_Id,
                        principalTable: "MerchantProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantRemitProductRate_Merchant_Id_fkey",
                        column: x => x.Merchant_Id,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MerchantRemitProductRate_Receive_Country_3_iso_fkey",
                        column: x => x.Receive_Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "MerchantRemitProductRate_Receive_Cur_fkey",
                        column: x => x.Receive_Cur,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                    table.ForeignKey(
                        name: "MerchantRemitProductRate_Send_Country_3_iso_fkey",
                        column: x => x.Send_Country_3_iso,
                        principalTable: "Country",
                        principalColumn: "Country_3_iso");
                    table.ForeignKey(
                        name: "MerchantRemitProductRate_Send_Cur_fkey",
                        column: x => x.Send_Cur,
                        principalTable: "Currency",
                        principalColumn: "CurrencyIso");
                });

            migrationBuilder.CreateTable(
                name: "CustomerUsedCoupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MerchantProductCoupon_Id = table.Column<int>(type: "integer", nullable: false),
                    Coupon_Code = table.Column<string>(type: "character varying", nullable: false),
                    Merchant_Id = table.Column<int>(type: "integer", nullable: true),
                    CustomerUser_Id = table.Column<int>(type: "integer", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    Used_DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomerUsedCoupon_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomerUsedCoupon_CustomerUser_Id_fkey",
                        column: x => x.CustomerUser_Id,
                        principalTable: "CustomerUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomerUsedCoupon_MerchantProductCoupon_Id_fkey",
                        column: x => x.MerchantProductCoupon_Id,
                        principalTable: "MerchantProductCoupon",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomerUsedCoupon_Merchant_Id_fkey",
                        column: x => x.Merchant_Id,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "IsAdmin", "IsCustomer", "IsDeleted", "IsMerchant", "IsVendor", "PermissionName" },
                values: new object[,]
                {
                    { 10, true, true, false, true, true, "CanAccessAdmin" },
                    { 11, false, false, false, true, true, "CanAccessMerchant" },
                    { 12, false, false, false, false, true, "CanAccessVendor" },
                    { 13, false, true, false, false, false, "CanAccessCustomer" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "RoleName" },
                values: new object[,]
                {
                    { 1, false, "Admin" },
                    { 2, false, "Merchant" },
                    { 3, false, "Vendor" },
                    { 4, false, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Description", "IsDeleted", "StatusCode" },
                values: new object[,]
                {
                    { 1, "Enabled", false, "EN" },
                    { 2, "Disabled", false, "DS" },
                    { 3, "Locked", false, "LK" },
                    { 4, "PasswordExpired", false, "PE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Phone", "StatusId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 23, 11, 20, 4, 920, DateTimeKind.Utc).AddTicks(3694), null, "carl.unni@fincompare.com", "Carl", false, "Unni", new byte[] { 232, 111, 120, 168, 163, 202, 240, 182, 13, 142, 116, 229, 148, 42, 166, 216, 109, 193, 80, 205, 60, 3, 51, 138, 239, 37, 183, 210, 215, 227, 172, 199 }, "1234567890", 1 },
                    { 2, new DateTime(2024, 7, 23, 11, 20, 4, 920, DateTimeKind.Utc).AddTicks(3707), null, "sailesh.pillai@fincompare.com", "Sailesh", false, "Pillai", new byte[] { 232, 111, 120, 168, 163, 202, 240, 182, 13, 142, 116, 229, 148, 42, 166, 216, 109, 193, 80, 205, 60, 3, 51, 138, 239, 37, 183, 210, 215, 227, 172, 199 }, "9876543216", 1 }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId", "IsDeleted" },
                values: new object[,]
                {
                    { 10, 1, false },
                    { 10, 2, false }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "IsDeleted" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 1, 2, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveAsset_AssetsMaster_Id",
                table: "ActiveAsset",
                column: "AssetsMaster_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveAsset_Country_3_iso",
                table: "ActiveAsset",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveAsset_Merchant_Id",
                table: "ActiveAsset",
                column: "Merchant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveAsset_ServiceCategory_Id",
                table: "ActiveAsset",
                column: "ServiceCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsMaster_Country_3_iso",
                table: "AssetsMaster",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_City_State_id",
                table: "City",
                column: "State_id");

            migrationBuilder.CreateIndex(
                name: "IX_ClickLead_Country_3_iso",
                table: "ClickLead",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_ClickLead_CustomerUser_Id",
                table: "ClickLead",
                column: "CustomerUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ClickLead_Merchant_Id",
                table: "ClickLead",
                column: "Merchant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrency_Country_3_iso",
                table: "CountryCurrency",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrency_CountryCurrencyCategory_id",
                table: "CountryCurrency",
                column: "CountryCurrencyCategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrency_Currency_3_iso",
                table: "CountryCurrency",
                column: "Currency_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRateSubscription_CustomerUser_Id",
                table: "CustomerRateSubscription",
                column: "CustomerUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRateSubscription_Receive_Cur",
                table: "CustomerRateSubscription",
                column: "Receive_Cur");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRateSubscription_Send_Cur",
                table: "CustomerRateSubscription",
                column: "Send_Cur");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReview_Merchant_Id",
                table: "CustomerReview",
                column: "Merchant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUsedCoupon_CustomerUser_Id",
                table: "CustomerUsedCoupon",
                column: "CustomerUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUsedCoupon_Merchant_Id",
                table: "CustomerUsedCoupon",
                column: "Merchant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUsedCoupon_MerchantProductCoupon_Id",
                table: "CustomerUsedCoupon",
                column: "MerchantProductCoupon_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUser_Country_3_iso",
                table: "CustomerUser",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUser_UserId",
                table: "CustomerUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMerchant_Country_3_iso",
                table: "GroupMerchant",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_Country_3_iso",
                table: "Instrument",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MarketRate_Receive_Cur",
                table: "MarketRate",
                column: "Receive_Cur");

            migrationBuilder.CreateIndex(
                name: "IX_MarketRate_Send_Cur",
                table: "MarketRate",
                column: "Send_Cur");

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_Country_3_iso",
                table: "Merchant",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_GroupMerchant_id",
                table: "Merchant",
                column: "GroupMerchant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_UserId",
                table: "Merchant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantCampaign_Merchant_Id",
                table: "MerchantCampaign",
                column: "Merchant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantCampaign_MerchantProduct_Id",
                table: "MerchantCampaign",
                column: "MerchantProduct_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantCampaign_Receive_Country_3_iso",
                table: "MerchantCampaign",
                column: "Receive_Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantCampaign_Send_Country_3_iso",
                table: "MerchantCampaign",
                column: "Send_Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantCampaign_ServiceCategory_Id",
                table: "MerchantCampaign",
                column: "ServiceCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProduct_Instrument_Id",
                table: "MerchantProduct",
                column: "Instrument_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProduct_Merchant_Id",
                table: "MerchantProduct",
                column: "Merchant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProduct_Product_Id",
                table: "MerchantProduct",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProduct_Receive_Country_3_iso",
                table: "MerchantProduct",
                column: "Receive_Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProduct_Receive_Currency_Id",
                table: "MerchantProduct",
                column: "Receive_Currency_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProduct_Send_Country_3_iso",
                table: "MerchantProduct",
                column: "Send_Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProduct_Send_Currency_Id",
                table: "MerchantProduct",
                column: "Send_Currency_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProduct_ServiceCategory_Id",
                table: "MerchantProduct",
                column: "ServiceCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProductCoupon_Coupon_Id",
                table: "MerchantProductCoupon",
                column: "Coupon_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProductCoupon_MerchantId",
                table: "MerchantProductCoupon",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProductCoupon_MerchantProduct_Id",
                table: "MerchantProductCoupon",
                column: "MerchantProduct_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductFee_Fees_Cur",
                table: "MerchantRemitProductFee",
                column: "Fees_Cur");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductFee_Merchant_Id",
                table: "MerchantRemitProductFee",
                column: "Merchant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductFee_MerchantProduct_Id",
                table: "MerchantRemitProductFee",
                column: "MerchantProduct_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductFee_Receive_Country_3_iso",
                table: "MerchantRemitProductFee",
                column: "Receive_Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductFee_Receive_Currency",
                table: "MerchantRemitProductFee",
                column: "Receive_Currency");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductFee_Send_Country_3_iso",
                table: "MerchantRemitProductFee",
                column: "Send_Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductFee_Send_Currency",
                table: "MerchantRemitProductFee",
                column: "Send_Currency");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductRate_Merchant_Id",
                table: "MerchantRemitProductRate",
                column: "Merchant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductRate_Merchant_Rate_Ref",
                table: "MerchantRemitProductRate",
                column: "Merchant_Rate_Ref",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductRate_MerchantProduct_Id",
                table: "MerchantRemitProductRate",
                column: "MerchantProduct_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductRate_Receive_Country_3_iso",
                table: "MerchantRemitProductRate",
                column: "Receive_Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductRate_Receive_Cur",
                table: "MerchantRemitProductRate",
                column: "Receive_Cur");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductRate_Send_Country_3_iso",
                table: "MerchantRemitProductRate",
                column: "Send_Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRemitProductRate_Send_Cur",
                table: "MerchantRemitProductRate",
                column: "Send_Cur");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Country_3_iso",
                table: "Product",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ServiceCategory_Id",
                table: "Product",
                column: "ServiceCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RateCard_Country_3_iso",
                table: "RateCard",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_Country_3_iso",
                table: "ServiceCategory",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_State_Country_3_iso",
                table: "State",
                column: "Country_3_iso");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedBy",
                table: "Users",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveAsset");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "ClickLead");

            migrationBuilder.DropTable(
                name: "CountryCurrency");

            migrationBuilder.DropTable(
                name: "CustomerRateSubscription");

            migrationBuilder.DropTable(
                name: "CustomerReview");

            migrationBuilder.DropTable(
                name: "CustomerUsedCoupon");

            migrationBuilder.DropTable(
                name: "MarketRate");

            migrationBuilder.DropTable(
                name: "MerchantCampaign");

            migrationBuilder.DropTable(
                name: "MerchantRemitProductFee");

            migrationBuilder.DropTable(
                name: "MerchantRemitProductRate");

            migrationBuilder.DropTable(
                name: "RateCard");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AssetsMaster");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "CountryCurrencyCategory");

            migrationBuilder.DropTable(
                name: "CustomerUser");

            migrationBuilder.DropTable(
                name: "MerchantProductCoupon");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "MerchantProduct");

            migrationBuilder.DropTable(
                name: "Instrument");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "GroupMerchant");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ServiceCategory");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
