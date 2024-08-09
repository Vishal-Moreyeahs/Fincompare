using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;
using Fincompare.Domain.Enums;

namespace Fincompare.Infrastructure.Services
{
    public class MerchantPermissionService : IMerchantPermissionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public MerchantPermissionService(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUserService)
        {
            _unitOfWork = unitOfWork;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<bool> CheckMerchantPermission(int merchantId)
        {
            var loggedInUser = await _authenticatedUserService.GetLoggedInUser();
            var merchant = await _unitOfWork.GetRepository<Merchant>().GetById(merchantId);
            var userRoles = (await _unitOfWork.GetRepository<UserRole>().GetAll()).Where(x => x.UserId == loggedInUser.Id).ToList();
            if (userRoles.Any(x => x.RoleId == (int)RoleEnum.Admin || x.RoleId == (int)RoleEnum.BackOfficeInterface))
            {
                return true;
            }
            if (loggedInUser.Id == merchant.UserId)
            {
                return true;
            }

            return false;
        }
    }
}
