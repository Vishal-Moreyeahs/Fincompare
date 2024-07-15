using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class City : DateBase
    {
        public int Id { get; set; }

        public string CityName { get; set; } = null!;

        public int StateId { get; set; }

        public bool Status { get; set; } = true;


        public virtual State State { get; set; } = null!;
    }

}
