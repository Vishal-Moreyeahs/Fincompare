using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class RateCard : ActionBase
    {
        public int Id { get; set; }

        public string Country3Iso { get; set; } = null!;

        public string Rate_Card { get; set; } = null!;

        public bool Status { get; set; }

        public virtual Country Country3IsoNavigation { get; set; } = null!;
    }

}
