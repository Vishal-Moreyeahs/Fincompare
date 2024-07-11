using Fincompare.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Fincompare.Infrastructure.Authentication
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionEnum permission)
        : base(policy: permission.ToString())
        {

        }
    }
}
