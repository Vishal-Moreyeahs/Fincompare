using Fincompare.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fincompare.Domain.Entities.UserManagementEntities
{
    public class Permission : ActionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMerchant { get; set; }
        public bool IsVendor { get; set; }
        public bool IsCustomer { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }


    }
}
