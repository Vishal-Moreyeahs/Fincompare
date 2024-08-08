using AutoMapper;
using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;
using Fincompare.Domain.Enums;

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
                    response.Success = false;
                    response.Message = "merchant delete failed";
                    return response;
                }

                //set isDelete = true
                merchant.IsDeleted = true;
                await _unitOfWork.GetRepository<Merchant>().Upsert(merchant);
                await _unitOfWork.SaveChangesAsync();

                //retreive the User table(get userid from merchant)
                if (merchant.UserId != null)
                    //{
                    //    var userResponse = await _userManagerServices.GetUserById((int)merchant.UserId);
                    //    var user = userResponse.Data;

                    //    user.IsDeleted = true;
                    //    await _unitOfWork.GetRepository<User>().Upsert(user);
                    //    await _unitOfWork.SaveChangesAsync();

                    //}
                    response.Success = true;
                response.Message = "merchant record deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("merchant delete failed");
            }

        }

        public async Task<ApiResponse<MerchantDto>> EditMerchantProfile(UpdateMerchantRequest model)
        {
            var response = new ApiResponse<MerchantDto>();
            try
            {
                // Fetch merchant from the repository
                var merchant = await _unitOfWork.GetRepository<Merchant>().GetById(model.Id);
                if (merchant == null)
                {
                    response.Message = "merchant update failed";
                    return response;
                }

                model.UserId = model.UserId.CheckValue(merchant.UserId);

                if (model.UserId != null)
                {
                    var cheekUserId = (await _unitOfWork.GetRepository<Merchant>().GetAll()).ToList();
                    if (cheekUserId.Any(x => x.UserId == model.UserId && x.Id != model.Id))
                    {
                        response.Success = false;
                        response.Message = string.Format(@"user id {0} already assigned to another merchant", model.UserId);
                        return response;
                    }
                }

                var userRole = (_unitOfWork.GetRepository<UserRole>().GetAllRelatedEntity())
                    .Where(x => x.UserId == model.UserId).ToList();
                if (!userRole.Any(x => x.RoleId == (int)RoleEnum.Merchant))
                {
                    response.Success = false;
                    response.Message = string.Format(@"User is not {0}", RoleEnum.Merchant.ToString());
                    return response;
                }


                // Map the updated data and save it in the database
                _mapper.Map(model, merchant);

                await _unitOfWork.GetRepository<Merchant>().Upsert(merchant);
                await _unitOfWork.SaveChangesAsync();

                // Prepare the response
                response.Success = true;
                response.Message = "merchant record updated successfully";
                response.Data = _mapper.Map<MerchantDto>(merchant);
            }
            catch (Exception ex)
            {
                // Log the exception details for troubleshooting
                // Logger.LogError(ex, "Merchant Update failed");
                response.Success = false;
                response.Message = "merchant update failed";
            }

            return response;
        }



        public async Task<ApiResponse<IEnumerable<MerchantDto>>> GetAllMerchants(int? groupMerchantId, int? merchantId, string? merchantType, string? countryIso3, bool? status)
        {
            var response = new ApiResponse<IEnumerable<MerchantDto>>();

            try
            {
                //Get All Merchant 
                var merchants = await _unitOfWork.GetRepository<Merchant>().GetAll();
                if (merchants == null)
                {
                    response.Success = false;
                    response.Message = "merchant fetch failed";
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
                if (!string.IsNullOrEmpty(merchantType))
                    merchants = merchants.Where(mp => mp.MerchantType.ToLower().Trim() == merchantType.ToLower().Trim());

                var merchantsResponse = _mapper.Map<IEnumerable<MerchantDto>>(merchants);

                if (merchantsResponse == null || merchantsResponse.ToList().Count == 0)
                {
                    response.Success = false;
                    response.Message = "merchant fetch failed";
                    return response;
                }

                response.Success = true;
                response.Data = merchantsResponse;
                response.Message = "merchant record fetched successfully";
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"merchant fetch failed {ex.Message}");
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
                    response.Success = false;
                    response.Message = "merchant fetch failed";
                    return response;
                }

                var merchantsResponse = _mapper.Map<MerchantDto>(merchants);
                response.Success = true;
                response.Data = merchantsResponse;
                response.Message = "merchant record fetched successfully";
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("merchant fetch failed");
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
                    response.Success = false;
                    response.Message = "merchant fetch failed";
                    return response;
                }

                var merchantsResponse = _mapper.Map<MerchantDto>(merchant);
                response.Success = true;
                response.Data = merchantsResponse;
                response.Message = "merchant record fetched successfully";
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("merchant fetch failed");
            }

        }

        public async Task<ApiResponse<MerchantDto>> AddMerchant(AddMerchantRequest model)
        {
            var response = new ApiResponse<MerchantDto>();

            try
            {



                //check if merchant already exist or not

                var merchants = await _unitOfWork.GetRepository<Merchant>().GetAll();
                var checkMerchant = merchants.FirstOrDefault(x => x.MerchantCsem.Trim().ToUpper() == model.MerchantCsem.Trim().ToUpper());
                if (checkMerchant != null)
                {
                    response.Success = false;
                    response.Message = "Merchant email " + model.MerchantCsem + "already exists.";
                    return response;
                }
                var checkPhone = merchants.FirstOrDefault(x => x.MerchantPh1 == model.MerchantPh1);
                var checkPhone2 = merchants.FirstOrDefault(x => x.MerchantPh2 == model.MerchantPh2);
                if (checkPhone != null)
                {
                    response.Success = false;
                    response.Message = "Merchant" + model.MerchantPh1 + "number already exists.";
                    return response;
                }
                if (checkPhone2 != null)
                {
                    response.Success = false;
                    response.Message = "Merchant" + model.MerchantPh2 + "number already exists.";
                    return response;
                }
                var checkAffiliatedId = merchants.FirstOrDefault(x => x.AffiliateId.Trim().ToUpper() == model.AffiliateId.Trim().ToUpper());
                if (checkAffiliatedId != null)
                {
                    response.Success = false;
                    response.Message = "Merchant Affiliated id" + model.AffiliateId + "already exists.";
                    return response;
                }

                var checkMerchantCsph = merchants.FirstOrDefault(x => x.MerchantCsph == model.MerchantCsph);
                //if not then check its group exist or not if not then create.(Assign it to group)
                var checkGroup = await _unitOfWork.GetRepository<GroupMerchant>().GetById(model.GroupMerchantId);
                if (checkGroup == null)
                {
                    response.Success = false;
                    response.Message = "Merchant" + model.MerchantCsph + " number already exists.";
                    return response;
                }
                var checkMerchanEm1 = merchants.FirstOrDefault(x => x.MerchantEm1.Trim().ToUpper() == model.MerchantEm1.Trim().ToUpper());
                if (checkMerchanEm1 == null)
                {
                    response.Success = false;
                    response.Message = "Merchant" + model.MerchantEm1 + " email already exists.";
                    return response;
                }
                var checkMerchanEm2 = merchants.FirstOrDefault(x => x.MerchantEm2.Trim().ToUpper() == model.MerchantEm2.Trim().ToUpper());
                if (checkMerchanEm2 == null)
                {
                    response.Success = false;
                    response.Message = "Merchant" + model.MerchantEm2 + " email already exists.";
                    return response;
                }
                var checkwebUrl = merchants.FirstOrDefault(x => x.WebUrl.Trim().ToUpper() == model.WebUrl.Trim().ToUpper());
                if (checkMerchanEm2 == null)
                {
                    response.Success = false;
                    response.Message = "Merchant" + model.WebUrl + "web url already exists.";
                    return response;
                }




                //Create a merchant and add this in Merchant Table.
                var merchant = _mapper.Map<Merchant>(model);
                //merchant.UserId = registerMerchant.Data.Id;
                await _unitOfWork.GetRepository<Merchant>().Add(merchant);
                await _unitOfWork.SaveChangesAsync();

                var merchantData = _mapper.Map<MerchantDto>(merchant);
                response.Success = true;
                response.Message = "Merchant record created successfully";
                response.Data = merchantData;
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
    public static class CheckNullSetting
    {
        public static T CheckValue<T>(this T value, T newValue)
        {
            return value ?? newValue;
        }
        public static int CheckValue(this int value, int newValue)
        {
            return value == 0 ? newValue : value;
        }
        public static int CheckValue(this int? value, int newValue)
        {
            return (value == 0 || value == null) ? newValue : value.Value;
        }
    }
}
