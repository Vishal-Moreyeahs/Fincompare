using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class CountryCurrencyCategory : DateBase
    {
        public int Id { get; set; }

        public string Definition { get; set; } = null!;

        public bool Status { get; set; }

        public virtual ICollection<CountryCurrency> CountryCurrencies { get; set; } = new List<CountryCurrency>();
    }

}
