using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Application.Response;

namespace Fincompare.Application.Services
{
    public class MerchantServices : IMerchantServices
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IUserManagerServices _userManagerServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public MerchantServices(IAuthenticatedUserService authenticatedUser, IUserManagerServices userManager,
                                    IUnitOfWork unitOfWork, IAuthService authService)
        {
            _authenticatedUserService = authenticatedUser;
            _userManagerServices = userManager;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public Task<ApiResponse<string>> DeleteMerchant(int merchantId)
        {
            //get merchant

            //set isDelete = true

            // retreive the User table (get userid from merchant)

            //set isDeleted = true in User


            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> EditMerchantProfile(UpdateMerchantRequest model)
        {
            //get merchant 

            //edit its profile and map and save it in database

            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<MerchantDto>>> GetAllMerchants()
        {
            //Get All Merchant 

            throw new NotImplementedException();
        }

        public Task<ApiResponse<MerchantDto>> GetMerchantByMerchantId(string merchantId)
        {
            // get merchant from merchant id
            throw new NotImplementedException();
        }

        public Task<ApiResponse<MerchantDto>> GetMerchantByUserId(int userId)
        {
            //Check user with id exist or not.

            //get from merchant with that userId
            throw new NotImplementedException();

        }

        public Task<ApiResponse<string>> OnboardMerchant(AddMerchantRequest model)
        {
            //check if merchant already exist or not

            //if not then check its group exist or not if not then create.

            //Create a user in userTable where merchant is created by admin.

            //Create a merchant and add this in Merchant Table.
            throw new NotImplementedException();

        }
    }
}
