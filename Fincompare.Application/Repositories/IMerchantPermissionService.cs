namespace Fincompare.Application.Repositories
{
    public interface IMerchantPermissionService
    {
        Task<bool> CheckMerchantPermission(int merchantId);
    }
}
