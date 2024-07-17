using Fincompare.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fincompare.Domain.Entities.UserManagementEntities
{
    public class User : ActionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedBy { get; set; }
        public int StatusId { get; set; }

        public virtual User CreatedUser { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual ICollection<User> CreatedUsers { get; set; } = new List<User>();
        public virtual ICollection<Merchant> Merchants { get; set; } = new List<Merchant>();
        public virtual ICollection<CustomerUser> CustomerUsers { get; set; } = new List<CustomerUser>();

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}
