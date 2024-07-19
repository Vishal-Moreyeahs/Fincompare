using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.Common;
using Fincompare.Domain.Entities.UserManagementEntities;
using Fincompare.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography;
using System.Text;

namespace Fincompare.Persitence
{

    public class FincompareDbContext : DbContext
    {
        public FincompareDbContext(DbContextOptions<FincompareDbContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }


        //DbSet or Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        //DbSet or Tables


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActiveAsset>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("ActiveAsset_pkey");

                entity.ToTable("ActiveAsset");

                entity.Property(e => e.AssetDescription).HasColumnName("Asset_Description").HasMaxLength(200);
                entity.Property(e => e.AssetMerchantUrl)
                    .HasColumnType("character varying")
                    .HasColumnName("Asset_Merchant_Url").HasMaxLength(200);

                entity.Property(e => e.AssetsMasterId).HasColumnName("AssetsMaster_Id");
                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.Date).HasColumnType("timestamp with time zone");
                entity.Property(e => e.DateActive)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Date_Active");
                entity.Property(e => e.DateValidity)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Date_Validity");
                entity.Property(e => e.MerchantId).HasColumnName("Merchant_Id");
                entity.Property(e => e.ServiceCategoryId).HasColumnName("ServiceCategory_Id");

                entity.HasOne(d => d.AssetsMaster).WithMany(p => p.ActiveAssets)
                    .HasForeignKey(d => d.AssetsMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ActiveAsset_AssetsMaster_Id_fkey");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.ActiveAssets)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ActiveAsset_Country_3_iso_fkey");

                entity.HasOne(d => d.Merchant).WithMany(p => p.ActiveAssets)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ActiveAsset_Merchant_Id_fkey");

                entity.HasOne(d => d.ServiceCategory).WithMany(p => p.ActiveAssets)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ActiveAsset_ServiceCategory_Id_fkey");
            });

            modelBuilder.Entity<AssetsMaster>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("AssetsMaster_pkey");

                entity.ToTable("AssetsMaster");

                entity.Property(e => e.AssetDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("Asset_Description").HasMaxLength(150);
                entity.Property(e => e.AssetName)
                    .HasColumnType("character varying")
                    .HasColumnName("Asset_Name").HasMaxLength(150);
                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.Date).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.AssetsMasters)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AssetsMaster_Country_3_iso_fkey");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("City_pkey");

                entity.ToTable("City");

                entity.Property(e => e.CityName)
                    .HasColumnType("character varying")
                    .HasColumnName("City_Name").HasMaxLength(100);
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.StateId).HasColumnName("State_id");
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.State).WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("City_State_id_fkey");
            });

            modelBuilder.Entity<ClickLead>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("ClickLead_pkey");

                entity.ToTable("ClickLead");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CustomerUserId).HasColumnName("CustomerUser_Id");
                entity.Property(e => e.Date).HasColumnType("timestamp with time zone");
                entity.Property(e => e.MerchantId).HasColumnName("Merchant_Id");
                entity.Property(e => e.RoutingParamters)
                    .HasColumnType("character varying")
                    .HasColumnName("Routing_Paramters").HasMaxLength(200);

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.ClickLeads)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClickLead_Country_3_iso_fkey");

                entity.HasOne(d => d.CustomerUser).WithMany(p => p.ClickLeads)
                    .HasForeignKey(d => d.CustomerUserId)
                    .HasConstraintName("ClickLead_CustomerUser_Id_fkey");

                entity.HasOne(d => d.Merchant).WithMany(p => p.ClickLeads)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClickLead_Merchant_Id_fkey");
            });

            modelBuilder.Entity<Country>(entity =>
            {


                entity.HasKey(e => e.Country3Iso).HasName("Country_pkey");

                entity.ToTable("Country");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.Country2Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_2_iso");
                entity.Property(e => e.CountryName)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_Name").HasMaxLength(40);
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");
                entity.Property(e => e.WebLink)
                    .HasColumnType("character varying")
                    .HasColumnName("Web_link").HasMaxLength(150);

            });

            modelBuilder.Entity<CountryCurrency>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("CountryCurrency_pkey");

                entity.ToTable("CountryCurrency");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CountryCurrencyCategoryId).HasColumnName("CountryCurrencyCategory_id");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.CurrencyIso).HasColumnName("Currency_3_iso");
                entity.Property(e => e.IsPrimaryCur).HasColumnName("IsPrimary_Cur");
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.CountryCurrencies)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CountryCurrency_Country_3_iso_fkey");

                entity.HasOne(d => d.CountryCurrencyCategory).WithMany(p => p.CountryCurrencies)
                    .HasForeignKey(d => d.CountryCurrencyCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CountryCurrency_CountryCurrencyCategory_id_fkey");

                entity.HasOne(d => d.Currency).WithMany(p => p.CountryCurrencies)
                    .HasForeignKey(d => d.CurrencyIso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CountryCurrency_Currency_3_iso_fkey");
            });

            modelBuilder.Entity<CountryCurrencyCategory>(entity =>
            {
                entity.HasKey(e => e.CountryCurrencyCategoryId).HasName("CountryCurrencyCategory_pkey");

                entity.ToTable("CountryCurrencyCategory");

                entity.Property(e => e.CountryCurrencyCategoryId)
                    .HasColumnName("Country_Currency_Category_Id").HasMaxLength(15);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.Definition).HasColumnType("character varying").HasMaxLength(35);
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Coupon_pkey");

                entity.ToTable("Coupon");

                entity.Property(e => e.CouponFormat)
                    .HasColumnType("character varying")
                    .HasColumnName("Coupon_Format").HasMaxLength(35);
                entity.Property(e => e.CouponName)
                    .HasColumnType("character varying")
                    .HasColumnName("Coupon_Name");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.CurrencyIso).HasName("Currency_pkey");

                entity.ToTable("Currency");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.CurrencyName)
                    .HasColumnType("character varying")
                    .HasColumnName("Currency_Name").HasMaxLength(11);
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");
                entity.Property(e => e.VolatilityRange).HasColumnName("Volatility_Range");
            });

            modelBuilder.Entity<CustomerRateSubscription>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("CustomerRateSubscription_pkey");

                entity.ToTable("CustomerRateSubscription");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_date");
                entity.Property(e => e.CustomerUserId).HasColumnName("CustomerUser_Id");
                entity.Property(e => e.ReceiveCur).HasColumnName("Receive_Cur");
                entity.Property(e => e.SendCur).HasColumnName("Send_Cur");
                entity.Property(e => e.WishRate).HasColumnName("Wish_Rate");

                entity.HasOne(d => d.CustomerUser).WithMany(p => p.CustomerRateSubscriptions)
                    .HasForeignKey(d => d.CustomerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerRateSubscription_CustomerUser_Id_fkey");

                entity.HasOne(d => d.ReceiveCurNavigation).WithMany(p => p.CustomerRateSubscriptionReceiveCurNavigations)
                    .HasForeignKey(d => d.ReceiveCur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerRateSubscription_Receive_Cur_fkey");

                entity.HasOne(d => d.SendCurNavigation).WithMany(p => p.CustomerRateSubscriptionSendCurNavigations)
                    .HasForeignKey(d => d.SendCur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerRateSubscription_Send_Cur_fkey");
            });

            modelBuilder.Entity<CustomerReview>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("CustomerReview_pkey");

                entity.ToTable("CustomerReview");

                entity.Property(e => e.MerchantId).HasColumnName("Merchant_Id");
                entity.Property(e => e.Review).HasColumnType("character varying").HasMaxLength(400);

                entity.HasOne(d => d.Merchant).WithMany(p => p.CustomerReviews)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerReview_Merchant_Id_fkey");
            });

            modelBuilder.Entity<CustomerUsedCoupon>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("CustomerUsedCoupon_pkey");

                entity.ToTable("CustomerUsedCoupon");

                entity.Property(e => e.CouponCode)
                    .HasColumnType("character varying")
                    .HasColumnName("Coupon_Code");
                entity.Property(e => e.CustomerUserId).HasColumnName("CustomerUser_Id");
                entity.Property(e => e.MerchantId).HasColumnName("Merchant_Id");
                entity.Property(e => e.MerchantProductCouponId).HasColumnName("MerchantProductCoupon_Id");
                entity.Property(e => e.UsedDateTime)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Used_DateTime");

                entity.HasOne(d => d.CustomerUser).WithMany(p => p.CustomerUsedCoupons)
                    .HasForeignKey(d => d.CustomerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerUsedCoupon_CustomerUser_Id_fkey");

                entity.HasOne(d => d.Merchant).WithMany(p => p.CustomerUsedCoupons)
                    .HasForeignKey(d => d.MerchantId)
                    .HasConstraintName("CustomerUsedCoupon_Merchant_Id_fkey");

                entity.HasOne(d => d.MerchantProductCoupon).WithMany(p => p.CustomerUsedCoupons)
                    .HasForeignKey(d => d.MerchantProductCouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerUsedCoupon_MerchantProductCoupon_Id_fkey");
            });

            modelBuilder.Entity<CustomerUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("CustomerUser_pkey");

                entity.ToTable("CustomerUser");

                entity.Property(e => e.Address).HasColumnType("character varying").HasMaxLength(35);
                entity.Property(e => e.City).HasColumnType("character varying");
                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.CustomerName).HasColumnType("character varying").HasMaxLength(40);
                entity.Property(e => e.EmailId)
                    .HasColumnType("character varying")
                    .HasColumnName("Email_Id").HasMaxLength(35);
                entity.Property(e => e.Phone).HasColumnType("character varying").HasMaxLength(16);
                entity.Property(e => e.PromoSubscription).HasColumnName("Promo_Subscription");
                entity.Property(e => e.RateSubscription).HasColumnName("Rate_Subscription");
                entity.Property(e => e.State).HasColumnType("character varying");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.CustomerUsers)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerUser_Country_3_iso_fkey");

                entity.HasOne(d => d.User).WithMany(p => p.CustomerUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerUser_UserId_fkey");
            });

            modelBuilder.Entity<GroupMerchant>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("GroupMerchant_pkey");

                entity.ToTable("GroupMerchant");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.GroupCsem)
                    .HasColumnType("character varying")
                    .HasColumnName("Group_CSEm").HasMaxLength(50);
                entity.Property(e => e.GroupCsph)
                    .HasColumnType("character varying")
                    .HasColumnName("Group_CSPh").HasMaxLength(15);
                entity.Property(e => e.GroupEm1)
                    .HasColumnType("character varying")
                    .HasColumnName("Group_Em1").HasMaxLength(50);
                entity.Property(e => e.GroupEm2)
                    .HasColumnType("character varying")
                    .HasColumnName("Group_Em2").HasMaxLength(50);
                entity.Property(e => e.GroupMerchantName)
                    .HasColumnType("character varying")
                    .HasColumnName("Group_Merchant_Name").HasMaxLength(55);
                entity.Property(e => e.GroupMerchantShortName)
                    .HasColumnType("character varying")
                    .HasColumnName("Group_Merchant_ShortName").HasMaxLength(25);
                entity.Property(e => e.GroupPh1)
                    .HasColumnType("character varying")
                    .HasColumnName("Group_Ph1").HasMaxLength(15);
                entity.Property(e => e.GroupPh2)
                    .HasColumnType("character varying")
                    .HasColumnName("Group_Ph2").HasMaxLength(15);
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.GroupMerchants)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupMerchant_Country_3_iso_fkey");
            });

            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Instrument_pkey");

                entity.ToTable("Instrument");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.InstrumentName)
                    .HasColumnType("character varying")
                    .HasColumnName("Instrument_Name").HasMaxLength(25);
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.Instruments)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Instrument_Country_3_iso_fkey");
            });

            modelBuilder.Entity<MarketRate>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("MarketRate_pkey");

                entity.ToTable("MarketRate");

                entity.Property(e => e.Date).HasColumnType("timestamp with time zone");
                entity.Property(e => e.RateSource)
                    .HasColumnType("character varying")
                    .HasColumnName("Rate_Source");
                entity.Property(e => e.ReceiveCur).HasColumnName("Receive_Cur");
                entity.Property(e => e.SendCur).HasColumnName("Send_Cur");

                entity.HasOne(d => d.ReceiveCurNavigation).WithMany(p => p.MarketRateReceiveCurNavigations)
                    .HasForeignKey(d => d.ReceiveCur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MarketRate_Currency_Id_fkey1");

                entity.HasOne(d => d.SendCurNavigation).WithMany(p => p.MarketRateSendCurNavigations)
                    .HasForeignKey(d => d.SendCur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MarketRate_Send_Cur_fkey");
            });

            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Merchant_pkey");

                entity.ToTable("Merchant");

                entity.Property(e => e.AffiliateId)
                    .HasColumnType("character varying")
                    .HasColumnName("Affiliate_Id").HasMaxLength(36);
                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.GroupMerchantId).HasColumnName("GroupMerchant_id");
                entity.Property(e => e.MerchantCsem)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_CSEm").HasMaxLength(50);
                entity.Property(e => e.MerchantCsph)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_CSPh").HasMaxLength(15);
                entity.Property(e => e.MerchantEm1)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_EM1").HasMaxLength(50);
                entity.Property(e => e.MerchantEm2)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_EM2").HasMaxLength(50);
                entity.Property(e => e.MerchantName)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_Name").HasMaxLength(75);
                entity.Property(e => e.MerchantPh1)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_Ph1").HasMaxLength(15);
                entity.Property(e => e.MerchantPh2)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_Ph2").HasMaxLength(15);
                entity.Property(e => e.MerchantShortName)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_ShortName").HasMaxLength(25);
                entity.Property(e => e.RoutingParameters)
                    .HasColumnType("character varying")
                    .HasColumnName("Routing_Parameters").HasMaxLength(200);
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");
                entity.Property(e => e.WebUrl)
                    .HasColumnType("character varying")
                    .HasColumnName("Web_Url").HasMaxLength(300);

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.Merchants)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Merchant_Country_3_iso_fkey");

                entity.HasOne(d => d.GroupMerchant).WithMany(p => p.Merchants)
                    .HasForeignKey(d => d.GroupMerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Merchant_GroupMerchant_id_fkey");

                entity.HasOne(d => d.User).WithMany(p => p.Merchants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Merchant_UserId_fkey");
            });

            modelBuilder.Entity<MerchantCampaign>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("MerchantCampaign_pkey");

                entity.ToTable("MerchantCampaign");

                entity.Property(e => e.CampaignCode)
                    .HasColumnType("character varying")
                    .HasColumnName("Campaign_Code").HasMaxLength(50);
                entity.Property(e => e.CampaignDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("Campaign_Description").HasMaxLength(300);
                entity.Property(e => e.Date).HasColumnType("timestamp with time zone");
                entity.Property(e => e.DateActive)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Date_Active");
                entity.Property(e => e.DateValidity)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Date_Validity");
                entity.Property(e => e.MerchantId).HasColumnName("Merchant_Id");
                entity.Property(e => e.MerchantProductId).HasColumnName("MerchantProduct_Id");
                entity.Property(e => e.ReceiveCountry3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Receive_Country_3_iso");
                entity.Property(e => e.SendCountry3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Send_Country_3_iso");
                entity.Property(e => e.ServiceCategoryId).HasColumnName("ServiceCategory_Id");

                entity.HasOne(d => d.Merchant).WithMany(p => p.MerchantCampaigns)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantCampaign_Merchant_Id_fkey");

                entity.HasOne(d => d.MerchantProduct).WithMany(p => p.MerchantCampaigns)
                    .HasForeignKey(d => d.MerchantProductId)
                    .HasConstraintName("MerchantCampaign_MerchantProduct_Id_fkey");

                entity.HasOne(d => d.ReceiveCountry3IsoNavigation).WithMany(p => p.MerchantCampaignReceiveCountry3IsoNavigations)
                    .HasForeignKey(d => d.ReceiveCountry3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantCampaign_Receive_Country_3_iso_fkey");

                entity.HasOne(d => d.SendCountry3IsoNavigation).WithMany(p => p.MerchantCampaignSendCountry3IsoNavigations)
                    .HasForeignKey(d => d.SendCountry3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantCampaign_Send_Country_3_iso_fkey");

                entity.HasOne(d => d.ServiceCategory).WithMany(p => p.MerchantCampaigns)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantCampaign_ServiceCategory_Id_fkey");
            });

            modelBuilder.Entity<MerchantProduct>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("MerchantProduct_pkey");

                entity.ToTable("MerchantProduct");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.InstrumentId).HasColumnName("Instrument_Id");
                entity.Property(e => e.MerchantId).HasColumnName("Merchant_Id");
                entity.Property(e => e.ProductId).HasColumnName("Product_Id");
                entity.Property(e => e.ReceiveCountry3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Receive_Country_3_iso");
                entity.Property(e => e.ReceiveCurrencyId).HasColumnName("Receive_Currency_Id");
                entity.Property(e => e.SendCountry3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Send_Country_3_iso");
                entity.Property(e => e.SendCurrencyId).HasColumnName("Send_Currency_Id");
                entity.Property(e => e.ServiceCategoryId).HasColumnName("ServiceCategory_Id");
                entity.Property(e => e.ServiceLevels)
                    .HasColumnType("character varying")
                    .HasColumnName("Service_Levels").HasMaxLength(6);
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Instrument).WithMany(p => p.MerchantProducts)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantProduct_Instrument_Id_fkey");

                entity.HasOne(d => d.Merchant).WithMany(p => p.MerchantProducts)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantProduct_Merchant_Id_fkey");

                entity.HasOne(d => d.Product).WithMany(p => p.MerchantProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantProduct_Product_Id_fkey");

                entity.HasOne(d => d.ReceiveCountry3IsoNavigation).WithMany(p => p.MerchantProductReceiveCountry3IsoNavigations)
                    .HasForeignKey(d => d.ReceiveCountry3Iso)
                    .HasConstraintName("MerchantProduct_Receive_Country_3_iso_fkey");

                entity.HasOne(d => d.ReceiveCurrency).WithMany(p => p.MerchantProductReceiveCurrencies)
                    .HasForeignKey(d => d.ReceiveCurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantProduct_Receive_Currency_Id_fkey");

                entity.HasOne(d => d.SendCountry3IsoNavigation).WithMany(p => p.MerchantProductSendCountry3IsoNavigations)
                    .HasForeignKey(d => d.SendCountry3Iso)
                    .HasConstraintName("MerchantProduct_Send_Country_3_iso_fkey");

                entity.HasOne(d => d.SendCurrency).WithMany(p => p.MerchantProductSendCurrencies)
                    .HasForeignKey(d => d.SendCurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantProduct_Send_Currency_Id_fkey");

                entity.HasOne(d => d.ServiceCategory).WithMany(p => p.MerchantProducts)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantProduct_ServiceCategory_Id_fkey");
            });

            modelBuilder.Entity<MerchantProductCoupon>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("MerchantProductCoupon_pkey");

                entity.ToTable("MerchantProductCoupon");

                entity.Property(e => e.CouponCode)
                    .HasColumnType("character varying")
                    .HasColumnName("Coupon_Code").HasMaxLength(10);
                entity.Property(e => e.CouponId).HasColumnName("Coupon_Id");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.MerchantProductId).HasColumnName("MerchantProduct_Id");
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");
                entity.Property(e => e.ValidityFrom).HasColumnType("timestamp with time zone");
                entity.Property(e => e.ValidityTo).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.Coupon).WithMany(p => p.MerchantProductCoupons)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantProductCoupon_Coupon_Id_fkey");

                entity.HasOne(d => d.MerchantProduct).WithMany(p => p.MerchantProductCoupons)
                    .HasForeignKey(d => d.MerchantProductId)
                    .HasConstraintName("MerchantProductCoupon_MerchantProduct_Id_fkey");
            });

            modelBuilder.Entity<MerchantRemitProductFee>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("MerchantRemitProductFee_pkey");

                entity.ToTable("MerchantRemitProductFee");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.FeesCur).HasColumnName("Fees_Cur");
                entity.Property(e => e.FeesName)
                    .HasColumnType("character varying")
                    .HasColumnName("Fees_Name").HasMaxLength(35);
                entity.Property(e => e.MerchantId).HasColumnName("Merchant_Id");
                entity.Property(e => e.MerchantProductId).HasColumnName("MerchantProduct_Id");
                entity.Property(e => e.PromoFees).HasColumnName("Promo_Fees");
                entity.Property(e => e.ReceiveCountry3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Receive_Country_3_iso");
                entity.Property(e => e.ReceiveCurrency).HasColumnName("Receive_Currency");
                entity.Property(e => e.ReceiveMaxLimit).HasColumnName("Receive_Max_Limit");
                entity.Property(e => e.ReceiveMinLimit).HasColumnName("Receive_Min_Limit");
                entity.Property(e => e.SendCountry3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Send_Country_3_iso");
                entity.Property(e => e.SendCurrency).HasColumnName("Send_Currency");
                entity.Property(e => e.SendMaxLimit).HasColumnName("Send_Max_Limit");
                entity.Property(e => e.SendMinLimit).HasColumnName("Send_Min_Limit");
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");
                entity.Property(e => e.ValidityExpiry).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.FeesCurNavigation).WithMany(p => p.MerchantRemitProductFeeFeesCurNavigations)
                    .HasForeignKey(d => d.FeesCur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductFee_Fees_Cur_fkey");

                entity.HasOne(d => d.Merchant).WithMany(p => p.MerchantRemitProductFees)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductFee_Merchant_Id_fkey");

                entity.HasOne(d => d.MerchantProduct).WithMany(p => p.MerchantRemitProductFees)
                    .HasForeignKey(d => d.MerchantProductId)
                    .HasConstraintName("MerchantRemitProductFee_MerchantProduct_Id_fkey");

                entity.HasOne(d => d.ReceiveCountry3IsoNavigation).WithMany(p => p.MerchantRemitProductFeeReceiveCountry3IsoNavigations)
                    .HasForeignKey(d => d.ReceiveCountry3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductFee_Receive_Country_3_iso_fkey");

                entity.HasOne(d => d.ReceiveCurrencyNavigation).WithMany(p => p.MerchantRemitProductFeeReceiveCurrencyNavigations)
                    .HasForeignKey(d => d.ReceiveCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductFee_Receive_Currency_fkey");

                entity.HasOne(d => d.SendCountry3IsoNavigation).WithMany(p => p.MerchantRemitProductFeeSendCountry3IsoNavigations)
                    .HasForeignKey(d => d.SendCountry3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductFee_Send_Country_3_iso_fkey");

                entity.HasOne(d => d.SendCurrencyNavigation).WithMany(p => p.MerchantRemitProductFeeSendCurrencyNavigations)
                    .HasForeignKey(d => d.SendCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductFee_Send_Currency_fkey");
            });

            modelBuilder.Entity<MerchantRemitProductRate>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("MerchantRemitProductRate_pkey");

                entity.ToTable("MerchantRemitProductRate");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.MerchantId).HasColumnName("Merchant_Id");
                entity.Property(e => e.MerchantProductId).HasColumnName("MerchantProduct_Id");
                entity.HasIndex(e => e.MerchantRateRef).IsUnique();
                entity.Property(e => e.MerchantRateRef)
                    .HasColumnType("character varying")
                    .HasColumnName("Merchant_Rate_Ref").HasMaxLength(36);
                entity.Property(e => e.PromoRate).HasColumnName("Promo_Rate");
                entity.Property(e => e.ReceiveCountry3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Receive_Country_3_iso");
                entity.Property(e => e.ReceiveCur).HasColumnName("Receive_Cur");
                entity.Property(e => e.ReceiveMaxLimit).HasColumnName("Receive_Max_Limit");
                entity.Property(e => e.ReceiveMinLimit).HasColumnName("Receive_Min_Limit");
                entity.Property(e => e.SendCountry3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Send_Country_3_iso");
                entity.Property(e => e.SendCur).HasColumnName("Send_Cur");
                entity.Property(e => e.SendMaxLimit).HasColumnName("Send_Max_Limit");
                entity.Property(e => e.SendMinLimit).HasColumnName("Send_Min_Limit");
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");
                entity.Property(e => e.ValidityExpiry)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Validity_Expiry");

                entity.HasOne(d => d.Merchant).WithMany(p => p.MerchantRemitProductRates)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductRate_Merchant_Id_fkey");

                entity.HasOne(d => d.MerchantProduct).WithMany(p => p.MerchantRemitProductRates)
                    .HasForeignKey(d => d.MerchantProductId)
                    .HasConstraintName("MerchantRemitProductRate_MerchantProduct_Id_fkey");

                entity.HasOne(d => d.ReceiveCountry3IsoNavigation).WithMany(p => p.MerchantRemitProductRateReceiveCountry3IsoNavigations)
                    .HasForeignKey(d => d.ReceiveCountry3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductRate_Receive_Country_3_iso_fkey");

                entity.HasOne(d => d.ReceiveCurNavigation).WithMany(p => p.MerchantRemitProductRateReceiveCurNavigations)
                    .HasForeignKey(d => d.ReceiveCur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductRate_Receive_Cur_fkey");

                entity.HasOne(d => d.SendCountry3IsoNavigation).WithMany(p => p.MerchantRemitProductRateSendCountry3IsoNavigations)
                    .HasForeignKey(d => d.SendCountry3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductRate_Send_Country_3_iso_fkey");

                entity.HasOne(d => d.SendCurNavigation).WithMany(p => p.MerchantRemitProductRateSendCurNavigations)
                    .HasForeignKey(d => d.SendCur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MerchantRemitProductRate_Send_Cur_fkey");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Product_pkey");

                entity.ToTable("Product");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.ProductDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("Product_Description").HasMaxLength(75);
                entity.Property(e => e.ProductName)
                    .HasColumnType("character varying")
                    .HasColumnName("Product_Name").HasMaxLength(25);
                entity.Property(e => e.ServiceCategoryId).HasColumnName("ServiceCategory_Id");
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.Products)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_Country_3_iso_fkey");

                entity.HasOne(d => d.ServiceCategory).WithMany(p => p.Products)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_ServiceCategory_Id_fkey");
            });

            modelBuilder.Entity<RateCard>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("RateCard_pkey");

                entity.ToTable("RateCard");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.Rate_Card)
                    .HasColumnType("character varying")
                    .HasColumnName("Rate_Card").HasMaxLength(7);

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.RateCards)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RateCard_Country_3_iso_fkey");
            });

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("ServiceCategory_pkey");

                entity.ToTable("ServiceCategory");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.ServCategoryName)
                    .HasColumnType("character varying")
                    .HasColumnName("Serv_Category_Name").HasMaxLength(25);
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.ServiceCategories)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ServiceCategory_Country_3_iso_fkey");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("State_pkey");

                entity.ToTable("State");

                entity.Property(e => e.Country3Iso)
                    .HasColumnType("character varying")
                    .HasColumnName("Country_3_iso");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_Date");
                entity.Property(e => e.StateName).HasColumnName("State_Name").HasMaxLength(100);
                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Country3IsoNavigation).WithMany(p => p.States)
                    .HasForeignKey(d => d.Country3Iso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("State_Country_3_iso_fkey");
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.Status)
                        .WithMany(s => s.Users)
                        .HasForeignKey(u => u.StatusId);

                entity.HasOne(d => d.CreatedUser).WithMany(p => p.CreatedUsers)
                                .HasForeignKey(d => d.CreatedBy)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_User_CreatedBy");

                entity.HasOne(d => d.Status).WithMany(p => p.Users)
                                .HasForeignKey(d => d.StatusId)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("User_StatusId_fkey");

                entity.HasData(
                        new User
                        {
                            Id = 1,
                            FirstName = "Aarya",
                            LastName = "Garg",
                            Email = "aarya.garg@moreyeahs.com",
                            Phone = "1234567890",
                            PasswordHash = EncryptPassword("aarya.garg@moreyeahs.comAdmin@123"),
                            CreatedAt = DateTime.UtcNow,
                            StatusId = (int)StatusEnum.Enabled,
                            CreatedBy = null
                        },
                        new User
                        {
                            Id = 2,
                            FirstName = "Vishal",
                            LastName = "Pawar",
                            Email = "vishal.pawar@moreyeahs.com",
                            Phone = "9876543216",
                            PasswordHash = EncryptPassword("vishal.pawar@moreyeahs.comAdmin@123"),
                            CreatedAt = DateTime.UtcNow,
                            StatusId = (int)StatusEnum.Enabled,
                            CreatedBy = null
                        }
                    );
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.StatusCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasData(
                    new Status { Id = (int)StatusEnum.Enabled, StatusCode = "EN", Description = StatusEnum.Enabled.ToString() },
                    new Status { Id = (int)StatusEnum.Disabled, StatusCode = "DS", Description = StatusEnum.Disabled.ToString() },
                    new Status { Id = (int)StatusEnum.Locked, StatusCode = "LK", Description = StatusEnum.Locked.ToString() },
                    new Status { Id = (int)StatusEnum.PasswordExpired, StatusCode = "PE", Description = StatusEnum.PasswordExpired.ToString() });
            });

            modelBuilder.Entity<Role>(entity =>
            {
                IEnumerable<Role> roles = Enum.GetValues<RoleEnum>()
                                            .Select(x => new Role
                                            {
                                                Id = (int)x,
                                                RoleName = x.ToString(),
                                            });

                entity.HasData(roles);
            });

            modelBuilder.Entity<Permission>(entity =>
            {

                entity.HasData(
                        new Permission { Id = (int)PermissionEnum.CanAccessAdmin, PermissionName = PermissionEnum.CanAccessAdmin.ToString(), IsAdmin = true },
                        new Permission { Id = (int)PermissionEnum.CanAccessMerchant, PermissionName = PermissionEnum.CanAccessMerchant.ToString(), IsMerchant = true },
                        new Permission { Id = (int)PermissionEnum.CanAccessVendor, PermissionName = PermissionEnum.CanAccessVendor.ToString(), IsVendor = true },
                        new Permission { Id = (int)PermissionEnum.CanAccessCustomer, PermissionName = PermissionEnum.CanAccessCustomer.ToString(), IsCustomer = true }
                    );
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });


                entity.HasData(
                        new UserRole { UserId = 1, RoleId = (int)RoleEnum.Admin },
                        new UserRole { UserId = 2, RoleId = (int)RoleEnum.Admin }
                    );
            });


            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            modelBuilder.Entity<UserPermission>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserPermission>()
                .HasOne(ur => ur.Permission)
                .WithMany(r => r.UserPermissions)
                .HasForeignKey(ur => ur.PermissionId);

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.PermissionId });

                entity.HasData(
                        new UserPermission { UserId = 1, PermissionId = (int)PermissionEnum.CanAccessAdmin },
                        new UserPermission { UserId = 2, PermissionId = (int)PermissionEnum.CanAccessAdmin }
                    );
            });


        }

        public byte[] EncryptPassword(string password)
        {
            // Initialize a SHA256 hash object
            SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<DateBase>())
            {
                var currentDateTime = DateTime.UtcNow;

                entry.Entity.CreatedDate = entry.State == EntityState.Added ? currentDateTime : entry.Entity.CreatedDate;
                entry.Entity.UpdatedDate = currentDateTime;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
