using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fincompare.Domain.Entities.UserManagementEntities
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string StatusCode { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
