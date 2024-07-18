using AutoMapper;
using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request;
using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;
using Fincompare.Domain.Enums;
using System.Diagnostics.Metrics;

namespace Fincompare.Application.Services
{
    public class MerchantServices : IMerchantServices
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IUserManagerServices _userManagerServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public MerchantServices(IAuthenticatedUserService authenticatedUser, IUserManagerServices userManager,
                                    IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper)
        {
            _authenticatedUserService = authenticatedUser;
            _userManagerServices = userManager;
            _unitOfWork = unitOfWork;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> DeleteMerchant(int merchantId)
        {
            try
            {
                var response = new ApiResponse<string>();
                //get merchant
                var merchant = await _unitOfWork.GetRepository<Merchant>().GetById(merchantId);
                if (merchant == null)
                {
                    response.Status = false;
                    response.Message = "Merchant not exists.";
                    return response;
                }

                //set isDelete = true
                merchant.IsDeleted = true;
                await _unitOfWork.GetRepository<Merchant>().Upsert(merchant);
                await _unitOfWork.SaveChangesAsync();

                // retreive the User table (get userid from merchant)
                if (merchant.UserId != null)
                { 
                    var userResponse = await _userManagerServices.GetUserById((int)merchant.UserId);
                    var user = userResponse.Data;

                    user.IsDeleted = true;
                    await _unitOfWork.GetRepository<User>().Upsert(user);
                    await _unitOfWork.SaveChangesAsync();

                }
                response.Status = true;
                response.Message = "Merchant Removed";
                return response;
            }
            catch (Exception ex) {
                throw new ApplicationException("Merchant Deletion Failed");
            }
            
        }

        public async Task<ApiResponse<IEnumerable<MerchantDto>>> EditMerchantProfile(UpdateMerchantRequest model)
        {
            var response = new ApiResponse<IEnumerable<MerchantDto>>();
            try
            {
                // Fetch merchant from the repository
                var merchant = await _unitOfWork.GetRepository<Merchant>().GetById(model.Id);
                if (merchant == null)
                {
                    response.Message = "Merchant Not Found";
                    return response;
                }

                // Map the updated data and save it in the database
                _mapper.Map(model, merchant);
                await _unitOfWork.GetRepository<Merchant>().Upsert(merchant);
                await _unitOfWork.SaveChangesAsync();

                // Prepare the response
                response.Status = true;
                response.Message = "Merchant Updated Successfully";
                response.Data = _mapper.Map<IEnumerable<MerchantDto>>(merchant);
            }
            catch (Exception ex)
            {
                // Log the exception details for troubleshooting
                // Logger.LogError(ex, "Merchant Update failed");
                response.Status = false;
                response.Message = "Merchant Update failed";
            }

            return response;
        }


        public async Task<ApiResponse<IEnumerable<MerchantDto>>> GetAllMerchants(int? groupMerchantId, int? merchantId, string? countryIso3, bool? status)
        {
            var response = new ApiResponse<IEnumerable<MerchantDto>>();

            try
            {
                //Get All Merchant 
                var merchants = await _unitOfWork.GetRepository<Merchant>().GetAll();
                if (merchants == null)
                {
                    response.Status = false;
                    response.Message = "Merchants not found";
                    return response;
                }

                if (groupMerchantId.HasValue)
                    merchants = merchants.Where(mp => mp.GroupMerchantId == groupMerchantId.Value);
                if (merchantId.HasValue)
                    merchants = merchants.Where(mp => mp.Id == merchantId.Value);
                if (!string.IsNullOrEmpty(countryIso3))
                    merchants = merchants.Where(mp => mp.Country3Iso == countryIso3);
                if (status.HasValue)
                    merchants = merchants.Where(mp => mp.Status == status.Value);

                var merchantsResponse = _mapper.Map<IEnumerable<MerchantDto>>(merchants);
                response.Status = true;
                response.Data = merchantsResponse;
                response.Message = "Merchants fetched successfully";
                return response;
            }
            catch (Exception ex) {

                throw new ApplicationException("Error in getting merchants");
            }
        }

        public async Task<ApiResponse<MerchantDto>> GetMerchantByMerchantId(int merchantId)
        {
            var response = new ApiResponse<MerchantDto>();

            try
            {
                //Get All Merchant 
                var merchants = await _unitOfWork.GetRepository<Merchant>().GetById(merchantId);
                if (merchants == null)
                {
                    response.Status = false;
                    response.Message = "Merchant not found";
                    return response;
                }

                var merchantsResponse = _mapper.Map<MerchantDto>(merchants);
                response.Status = true;
                response.Data = merchantsResponse;
                response.Message = "Merchant found";
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error in getting merchant");
            }
        }

        public async Task<ApiResponse<MerchantDto>> GetMerchantByUserId(int userId)
        {
            var response = new ApiResponse<MerchantDto>();

            try
            {
                //Get All Merchant 
                var merchants = await _unitOfWork.GetRepository<Merchant>().GetAll();
                var merchant = merchants.FirstOrDefault(x => x.UserId == userId);
                if (merchant == null)
                {
                    response.Status = false;
                    response.Message = "Merchant not found";
                    return response;
                }

                var merchantsResponse = _mapper.Map<MerchantDto>(merchants);
                response.Status = true;
                response.Data = merchantsResponse;
                response.Message = "Merchant found";
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error in getting merchant by user id");
            }

        }

        public async Task<ApiResponse<IEnumerable<MerchantDto>>> AddMerchant(AddMerchantRequest model)
        {
            var response = new ApiResponse<IEnumerable<MerchantDto>>();
            
            try
            {
                //check if merchant already exist or not

                var merchants = await _unitOfWork.GetRepository<Merchant>().GetAll();
                var checkMerchant = merchants.FirstOrDefault(x => x.MerchantCsem == model.MerchantCsem);
                if (checkMerchant != null)
                { 
                    response.Status = false;
                    response.Message = "Merchant email already exists.";
                    return response;
                }


                //if not then check its group exist or not if not then create.(Assign it to group)
                var checkGroup = await _unitOfWork.GetRepository<GroupMerchant>().GetById(model.GroupMerchantId);
                if (checkGroup == null)
                {
                    response.Status = false;
                    response.Message = "Invalid Group. Please enter valid group id.";
                    return response;
                }


                //Create a user in userTable where merchant is created by admin.
                //var loggedInUser = await _authenticatedUserService.GetLoggedInUser();
                //var merchantUser = new RegisterUserRequest()
                //{
                //    FirstName = model.MerchantName,
                //    LastName = model.MerchantName + " "+ model.MerchantShortName,
                //    Password = string.Concat(model.MerchantCsem,"Merchant@123"),
                //    Role = RoleEnum.Customer.ToString(), 
                //    Email = model.MerchantCsem.ToString(),
                //    CreatedBy = loggedInUser !=null ? loggedInUser.Id : null,
                //    Phone = model.MerchantCsph.ToString()
                //};
                //var registerMerchant = await _authService.Register(merchantUser);


                //Create a merchant and add this in Merchant Table.
                var merchant = _mapper.Map<Merchant>(model);
                //merchant.UserId = registerMerchant.Data.Id;
                await _unitOfWork.GetRepository<Merchant>().Add(merchant);
                await _unitOfWork.SaveChangesAsync();

                var merchantData = _mapper.Map<IEnumerable<MerchantDto>>(merchant);
                response.Status = true;
                response.Message = "Merchant created successfully";
                response.Data = merchantData;
                return response;
            }
            catch (Exception ex) {

                throw new ApplicationException("Merchant Onboarding Failed "+ex.Message);
            }

        }
    }
}
