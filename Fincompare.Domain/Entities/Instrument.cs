using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class Instrument : DateBase
    {
        public int Id { get; set; }

        public string InstrumentName { get; set; } = null!;

        public string Country3Iso { get; set; } = null!;
        public string? InstrumentType { get; set; }

        public bool Status { get; set; }

        public virtual Country Country3IsoNavigation { get; set; } = null!;

        public virtual ICollection<MerchantProduct> MerchantProducts { get; set; } = new List<MerchantProduct>();
        public virtual ICollection<MerchantRemitProductFee> MerchantRemitProductFees { get; set; } = new List<MerchantRemitProductFee>();
    }

}
