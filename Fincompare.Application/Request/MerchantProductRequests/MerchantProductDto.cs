namespace Fincompare.Application.Request.MerchantProductRequests
{
    public class MerchantProductDto
    {
        public int Id { get; set; }

        public int ServiceCategoryId { get; set; }

        public int InstrumentId { get; set; }

        public int ProductId { get; set; }

        public int MerchantId { get; set; }

        public string? SendCountry3Iso { get; set; }

        public string? ReceiveCountry3Iso { get; set; }

        public string SendCurrencyId { get; set; }

        public string ReceiveCurrencyId { get; set; }

        public bool Status { get; set; }

        public string ServiceLevels { get; set; } = null!;
    }
}
