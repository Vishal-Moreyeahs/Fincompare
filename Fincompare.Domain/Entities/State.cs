using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class State : Base
    {
        public int Id { get; set; }

        public int StateName { get; set; }

        public bool Status { get; set; }

        public string Country3Iso { get; set; } = null!;

        public virtual ICollection<City> Cities { get; set; } = new List<City>();

        public virtual Country Country3IsoNavigation { get; set; } = null!;
    }

}
