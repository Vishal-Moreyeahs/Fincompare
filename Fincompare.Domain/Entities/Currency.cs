using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class Currency : Base
    {
        public int Id { get; set; }

        public string CurrencyName { get; set; } = null!;

        public string? CurrencyIso { get; set; } 

        public int Decimal { get; set; }

        public bool Status { get; set; } 

        public int VolatilityRange { get; set; }

        public virtual ICollection<CountryCurrency> CountryCurrencies { get; set; } = new List<CountryCurrency>();

        public virtual ICollection<CustomerRateSubscription> CustomerRateSubscriptionReceiveCurNavigations { get; set; } = new List<CustomerRateSubscription>();

        public virtual ICollection<CustomerRateSubscription> CustomerRateSubscriptionSendCurNavigations { get; set; } = new List<CustomerRateSubscription>();

        public virtual ICollection<MarketRate> MarketRateReceiveCurNavigations { get; set; } = new List<MarketRate>();

        public virtual ICollection<MarketRate> MarketRateSendCurNavigations { get; set; } = new List<MarketRate>();

        public virtual ICollection<MerchantProduct> MerchantProductReceiveCurrencies { get; set; } = new List<MerchantProduct>();

        public virtual ICollection<MerchantProduct> MerchantProductSendCurrencies { get; set; } = new List<MerchantProduct>();

        public virtual ICollection<MerchantRemitProductFee> MerchantRemitProductFeeFeesCurNavigations { get; set; } = new List<MerchantRemitProductFee>();

        public virtual ICollection<MerchantRemitProductFee> MerchantRemitProductFeeReceiveCurrencyNavigations { get; set; } = new List<MerchantRemitProductFee>();

        public virtual ICollection<MerchantRemitProductFee> MerchantRemitProductFeeSendCurrencyNavigations { get; set; } = new List<MerchantRemitProductFee>();

        public virtual ICollection<MerchantRemitProductRate> MerchantRemitProductRateReceiveCurNavigations { get; set; } = new List<MerchantRemitProductRate>();

        public virtual ICollection<MerchantRemitProductRate> MerchantRemitProductRateSendCurNavigations { get; set; } = new List<MerchantRemitProductRate>();
    }

}
