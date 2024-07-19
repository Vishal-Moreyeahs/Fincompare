using Fincompare.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Fincompare.Domain.Entities
{
    public partial class CountryCurrencyCategory : DateBase
    {
        [Key]
        public string CountryCurrencyCategoryId { get; set; }

        public string Definition { get; set; } = null!;

        public bool Status { get; set; }

        public virtual ICollection<CountryCurrency> CountryCurrencies { get; set; } = new List<CountryCurrency>();
    }

}
