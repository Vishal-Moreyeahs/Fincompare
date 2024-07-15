using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class CountryCurrency : Base
    {
        public int Id { get; set; }

        public string Country3Iso { get; set; } = null!;

        public string? CurrencyIso { get; set; }

        public bool IsPrimaryCur { get; set; }

        public int? CountryCurrencyCategoryId { get; set; }

        public bool Status { get; set; } = true;

        public virtual Country Country3IsoNavigation { get; set; } = null!;

        public virtual CountryCurrencyCategory CountryCurrencyCategory { get; set; } = null!;

        public virtual Currency Currency { get; set; } = null!;
    }

}
