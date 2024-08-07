using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class MerchantRemitProductRate : DateBase
    {
        public int Id { get; set; }

        public string MerchantRateRef { get; set; } = null!;

        public int MerchantId { get; set; }

        public int? MerchantProductId { get; set; }

        public string SendCountry3Iso { get; set; } = null!;

        public string ReceiveCountry3Iso { get; set; } = null!;

        public string SendCur { get; set; }

        public string ReceiveCur { get; set; }

        public decimal SendMinLimit { get; set; }

        public decimal SendMaxLimit { get; set; }

        public decimal ReceiveMinLimit { get; set; }

        public decimal ReceiveMaxLimit { get; set; }

        public double Rate { get; set; }

        public double PromoRate { get; set; }

        public DateTime ValidityExpiry { get; set; }

        public bool Status { get; set; }

        public virtual Merchant Merchant { get; set; } = null!;

        public virtual MerchantProduct? MerchantProduct { get; set; }

        public virtual Country ReceiveCountry3IsoNavigation { get; set; } = null!;

        public virtual Currency ReceiveCurNavigation { get; set; } = null!;

        public virtual Country SendCountry3IsoNavigation { get; set; } = null!;

        public virtual Currency SendCurNavigation { get; set; } = null!;
    }

}
