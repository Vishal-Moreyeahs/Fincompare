using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class Product : DateBase
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public int ServiceCategoryId { get; set; }

        //public string Country3Iso { get; set; } = null!;

        public bool Status { get; set; }

        //public virtual Country Country3IsoNavigation { get; set; } = null!;

        public virtual ICollection<MerchantProduct> MerchantProducts { get; set; } = new List<MerchantProduct>();

        public virtual ServiceCategory ServiceCategory { get; set; } = null!;
    }

}
