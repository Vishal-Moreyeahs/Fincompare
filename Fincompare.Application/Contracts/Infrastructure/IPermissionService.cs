namespace Fincompare.Application.Contracts.Infrastructure
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(int userId);
    }
}
