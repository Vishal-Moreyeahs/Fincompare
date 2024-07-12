using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class State : Base
    {
        public int Id { get; set; }

        public string StateName { get; set; }

        public bool Status { get; set; } = true;

        public string Country3Iso { get; set; } = null!;

        public virtual ICollection<City> Cities { get; set; } = new List<City>();

        public virtual Country Country3IsoNavigation { get; set; } = null!;
    }

}
