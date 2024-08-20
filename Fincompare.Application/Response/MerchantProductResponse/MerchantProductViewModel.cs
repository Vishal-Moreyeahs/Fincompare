namespace Fincompare.Application.Response.MerchantProductResponse
{
    public class MerchantProductViewModel
    {
        public int Id { get; set; }

        public int ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }

        public int PayoutInstrumentId { get; set; }
        public string PayoutInstrumentName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int MerchantId { get; set; }
        public string MerchantName { get; set; }

        public string? SendCountry3Iso { get; set; }

        public string? ReceiveCountry3Iso { get; set; }

        public string SendCurrencyId { get; set; }

        public string ReceiveCurrencyId { get; set; }

        public bool Status { get; set; }

        public string ServiceLevels { get; set; } = null!;
        public bool IsFeeAdded { get; set; }
    }
}
