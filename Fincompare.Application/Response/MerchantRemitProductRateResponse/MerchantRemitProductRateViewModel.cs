namespace Fincompare.Application.Response.MerchantRemitProductRateResponse
{
    public class MerchantRemitProductRateViewModel
    {
        public int RemittanceRateId { get; set; }

        public string MerchantRateRef { get; set; } = null!;

        public int MerchantId { get; set; }

        public string MerchantName { get; set; }

        public int? MerchantProductId { get; set; }

        public int ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }

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
    }
}
