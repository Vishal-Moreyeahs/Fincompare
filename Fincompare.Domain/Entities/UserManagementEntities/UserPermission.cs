using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities.UserManagementEntities
{
    public class UserPermission : ActionBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
