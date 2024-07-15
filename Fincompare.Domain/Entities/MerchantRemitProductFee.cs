using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class MerchantRemitProductFee : DateBase
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }

        public string FeesName { get; set; } = null!;

        public string FeesCur { get; set; }

        public double Fees { get; set; }

        public double? PromoFees { get; set; }

        public int? MerchantProductId { get; set; }

        public string SendCountry3Iso { get; set; } = null!;

        public string ReceiveCountry3Iso { get; set; } = null!;

        public string SendCurrency { get; set; }

        public string ReceiveCurrency { get; set; }

        public double SendMinLimit { get; set; }

        public double SendMaxLimit { get; set; }

        public double ReceiveMinLimit { get; set; }

        public double ReceiveMaxLimit { get; set; }

        public DateTime ValidityExpiry { get; set; }

        public bool Status { get; set; }

        public virtual Currency FeesCurNavigation { get; set; } = null!;

        public virtual Merchant Merchant { get; set; } = null!;

        public virtual MerchantProduct? MerchantProduct { get; set; }

        public virtual Country ReceiveCountry3IsoNavigation { get; set; } = null!;

        public virtual Currency ReceiveCurrencyNavigation { get; set; } = null!;

        public virtual Country SendCountry3IsoNavigation { get; set; } = null!;

        public virtual Currency SendCurrencyNavigation { get; set; } = null!;
    }

}
