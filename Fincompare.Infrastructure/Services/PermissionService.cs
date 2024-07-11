using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _context;

        public PermissionService(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(int userId)
        {
            var permissions = await _context.GetRepository<Permission>().GetAll();

            var userPermissions = await _context.GetRepository<UserPermission>().GetAll();
            var functionNames = (from f in permissions
                                 join uf in userPermissions
                                 on f.Id equals uf.PermissionId
                                 where uf.UserId == userId
                                 select f.PermissionName).ToHashSet<string>();

            return functionNames;
        }
    }
}
