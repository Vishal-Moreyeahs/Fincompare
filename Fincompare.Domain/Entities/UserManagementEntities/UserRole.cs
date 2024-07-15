using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities.UserManagementEntities
{
    public class UserRole : ActionBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
