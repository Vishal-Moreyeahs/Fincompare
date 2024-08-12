using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.ActiveAssetRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.ActiveAssetResponse;
using Fincompare.Domain.Entities;

namespace Fincompare.Application.Services
{
    public class ActiveAssetService : IActiveAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActiveAssetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ActiveAssetResponseViewModel>> AddActiveAssetMerchant(AddActiveAssetRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<ActiveAssetResponseViewModel>() { Success = false, Message = "Active Asset creation failed" };

                var requestData = _mapper.Map<ActiveAsset>(model);

                await _unitOfWork.GetRepository<ActiveAsset>().Add(requestData);
                await _unitOfWork.SaveChangesAsync();

                var data = _mapper.Map<ActiveAssetResponseViewModel>(requestData);

                return new ApiResponse<ActiveAssetResponseViewModel>() { Success = true, Message = "Active asset record created successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException($"active asset creation failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<ActiveAssetResponseViewModel>>> GetAllActiveAssetRecord(int? assetMasterId, int? merchantId, bool? status, string? countryIso3)
        {
            var response = new ApiResponse<IEnumerable<ActiveAssetResponseViewModel>>();

            try
            {
                var dateTime = DateTime.UtcNow;

                //Get All Merchant 
                var activeAssets = await _unitOfWork.GetRepository<ActiveAsset>().GetAll();
                activeAssets = activeAssets.Where(x => x.DateValidity >= dateTime);
                if (activeAssets == null)
                {
                    response.Success = false;
                    response.Message = "active asset fetch failed";
                    return response;
                }


                if (assetMasterId.HasValue)
                    activeAssets = activeAssets.Where(mp => mp.AssetsMasterId == assetMasterId.Value);
                //if (serviceCategoryId.HasValue)
                //    activeAssets = activeAssets.Where(mp => mp.ServiceCategoryId == serviceCategoryId.Value);
                if (merchantId.HasValue)
                    activeAssets = activeAssets.Where(mp => mp.Id == merchantId.Value);
                //if (!string.IsNullOrEmpty(countryIso3))
                //    activeAssets = activeAssets.Where(mp => mp.Country3Iso == countryIso3);
                if (status.HasValue)
                    activeAssets = activeAssets.Where(mp => mp.Status == status.Value);
                if (!string.IsNullOrEmpty(countryIso3))
                    activeAssets = activeAssets.Where(mp => mp.Country3Iso == countryIso3);
                //if (dateActive.HasValue)
                //    activeAssets = activeAssets.Where(mp => mp.DateActive >= dateActive.Value);
                //if (dateValidity.HasValue)
                //    activeAssets = activeAssets.Where(mp => mp.DateValidity <= dateValidity.Value);

                var merchantsResponse = _mapper.Map<IEnumerable<ActiveAssetResponseViewModel>>(activeAssets);

                if (merchantsResponse == null || merchantsResponse.ToList().Count == 0)
                {
                    response.Success = false;
                    response.Message = "active asset fetch failed";
                    return response;
                }
                response.Success = true;
                response.Data = merchantsResponse;
                response.Message = "active asset record fetched successfully";
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"active asset fetch failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<ActiveAssetResponseViewModel>> UpdateActiveAssetMerchant(UpdateActiveAssetRequest model)
        {
            var response = new ApiResponse<ActiveAssetResponseViewModel>();
            try
            {
                // Fetch merchant from the repository
                var activeAsset = await _unitOfWork.GetRepository<ActiveAsset>().GetById(model.Id);
                if (activeAsset == null)
                {
                    response.Message = "active asset update failed";
                    return response;
                }

                // Map the updated data and save it in the database
                _mapper.Map(model, activeAsset);
                await _unitOfWork.GetRepository<ActiveAsset>().Upsert(activeAsset);
                await _unitOfWork.SaveChangesAsync();

                // Prepare the response
                response.Success = true;
                response.Message = "active asset record updated successfully";
                response.Data = _mapper.Map<ActiveAssetResponseViewModel>(activeAsset);
            }
            catch (Exception ex)
            {
                // Log the exception details for troubleshooting
                // Logger.LogError(ex, "Merchant Update failed");
                response.Success = false;
                response.Message = "active asset update failed";
            }

            return response;
        }
    }
}
