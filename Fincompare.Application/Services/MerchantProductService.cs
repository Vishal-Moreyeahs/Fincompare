using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantProductRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantProductResponse;
using Fincompare.Domain.Entities;

namespace Fincompare.Application.Services
{
    public class MerchantProductService : IMerchantProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMerchantPermissionService _merchantPermissionService;

        public MerchantProductService(IUnitOfWork unitOfWork, IMapper mapper, IMerchantPermissionService merchantPermissionService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _merchantPermissionService = merchantPermissionService;
        }

        public async Task<ApiResponse<MerchantProductViewModel>> AddMerchantProduct(AddMerchantProductRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = "merchant product creation failed"
                    };

                var isAuthenticatedMerchant = await _merchantPermissionService.CheckMerchantPermission(model.MerchantId);
                if (!isAuthenticatedMerchant)
                {
                    return new ApiResponse<MerchantProductViewModel>() { Success = false, Message = "Invalid/Unauthorized merchant" };

                }

                var instrument = await _unitOfWork.GetRepository<Instrument>().GetById(model.PayoutInstrumentId);
                if (instrument == null || instrument.InstrumentType.Trim().ToLower() != "payout")
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = "Invalid instrument type. instrument must be payout"
                    };
                }
                var merchant = await _unitOfWork.GetRepository<Merchant>().GetById(model.MerchantId);

                if (merchant == null)
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = $"merchant with id {model.MerchantId} not found "
                    };
                }

                if (merchant.Country3Iso.Trim().ToLower() != model.SendCountry3Iso.Trim().ToLower())
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = $"merchant country not matches with send country."
                    };
                }

                var isRecordExist = await DoesRecordExistAsync(model.ServiceCategoryId, model.PayoutInstrumentId, model.ProductId, model.MerchantId, model.SendCountry3Iso, model.ReceiveCountry3Iso, model.SendCurrencyId, model.ReceiveCurrencyId);

                if (isRecordExist)
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = $"merchant product already exist with these details."
                    };
                }

                //var checkMerchantExist = await _unitOfWork.GetRepository<Merchant>().GetById(model.MerchantId);
                var createdData = _mapper.Map<MerchantProduct>(model);
                await _unitOfWork.GetRepository<MerchantProduct>().Add(createdData);
                await _unitOfWork.SaveChangesAsync();
                var responseData = await _unitOfWork.GetRepository<MerchantProduct>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(createdData.Id);

                var merchantResponseData = new MerchantProductViewModel
                {
                    Id = responseData.Id,
                    MerchantId = responseData.MerchantId,
                    ServiceCategoryId = responseData.ServiceCategoryId,
                    ServiceCategoryName = responseData.ServiceCategory.ServCategoryName,
                    PayoutInstrumentId = responseData.InstrumentId,
                    PayoutInstrumentName = responseData.Instrument.InstrumentName,
                    ProductId = responseData.ProductId,
                    ProductName = responseData.Product.ProductName,
                    MerchantName = responseData.Merchant.MerchantName,
                    ReceiveCountry3Iso = responseData.ReceiveCountry3Iso,
                    SendCountry3Iso = responseData.SendCountry3Iso,
                    ReceiveCurrencyId = responseData.ReceiveCurrencyId,
                    SendCurrencyId = responseData.SendCurrencyId,
                    ServiceLevels = responseData.ServiceLevels,
                    IsFeeAdded = responseData.IsFeeAdded,
                    Status = responseData.Status
                };


                var response = new ApiResponse<MerchantProductViewModel>()
                {
                    Success = true,
                    Message = "merchant product record created successfully",
                    Data = merchantResponseData

                };
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("merchant product creation failed");
            }
        }

        public async Task<ApiResponse<IEnumerable<MerchantProductViewModel>>> GetMerchantProductByMerchantId(int merchantId)
        {
            try
            {
                var response = new ApiResponse<IEnumerable<MerchantProductViewModel>>();

                //Get Merchant
                var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity()
                                            .Where(x => x.MerchantId == merchantId)
                                                .Select(x => new MerchantProductViewModel
                                                {
                                                    Id = x.Id,
                                                    MerchantId = merchantId,
                                                    ServiceCategoryId = x.ServiceCategoryId,
                                                    ServiceCategoryName = x.ServiceCategory.ServCategoryName,
                                                    PayoutInstrumentId = x.InstrumentId,
                                                    PayoutInstrumentName = x.Instrument.InstrumentName,
                                                    ProductId = x.ProductId,
                                                    ProductName = x.Product.ProductName,
                                                    MerchantName = x.Merchant.MerchantName,
                                                    ReceiveCountry3Iso = x.ReceiveCountry3Iso,
                                                    SendCountry3Iso = x.SendCountry3Iso,
                                                    ReceiveCurrencyId = x.ReceiveCurrencyId,
                                                    SendCurrencyId = x.SendCurrencyId,
                                                    ServiceLevels = x.ServiceLevels,
                                                    IsFeeAdded = x.IsFeeAdded,
                                                    Status = x.Status
                                                }).ToList();

                if (merchantProductList == null || merchantProductList.Count == 0)
                {
                    response.Message = "merchant product fetch failed";
                    return response;
                }
                response.Success = true;
                response.Message = "merchant product record fetched successfully";
                response.Data = merchantProductList;
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("merchant product fetch failed");
            }

        }

        public async Task<ApiResponse<IEnumerable<MerchantProductViewModel>>> GetMerchantProducts(string sendCountry, string receiveCountry, string sendCurrency, string receiveCurrency, int? merchantID, int? merchantProductID, int? productID, int? serviceCategoryID, int? instrumentID, bool? status)
        {
            var response = new ApiResponse<IEnumerable<MerchantProductViewModel>>();
            var merchantProducts = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();


            // Apply filters
            if (merchantID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.MerchantId == merchantID.Value);
            if (merchantProductID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.Id == merchantProductID.Value);
            if (productID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.ProductId == productID.Value);
            if (serviceCategoryID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.ServiceCategoryId == serviceCategoryID.Value);
            if (instrumentID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.InstrumentId == instrumentID.Value);
            if (status.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.Status == status.Value);

            // Filter by required path parameters
            merchantProducts = merchantProducts
                .Where(mp => mp.SendCountry3Iso == sendCountry)
                .Where(mp => mp.ReceiveCountry3Iso == receiveCountry)
                .Where(mp => mp.SendCurrencyId == sendCurrency)
                .Where(mp => mp.ReceiveCurrencyId == receiveCurrency);

            if (!merchantProducts.Any())
            {
                response.Message = "merchant product fetch failed";
                return response;
            }
            var data = merchantProducts.Select(x => new MerchantProductViewModel
            {
                Id = x.Id,
                MerchantId = x.MerchantId,
                ServiceCategoryId = x.ServiceCategoryId,
                ServiceCategoryName = x.ServiceCategory.ServCategoryName,
                PayoutInstrumentId = x.InstrumentId,
                PayoutInstrumentName = x.Instrument.InstrumentName,
                ProductId = x.ProductId,
                ProductName = x.Product.ProductName,
                MerchantName = x.Merchant.MerchantName,
                ReceiveCountry3Iso = x.ReceiveCountry3Iso,
                SendCountry3Iso = x.SendCountry3Iso,
                ReceiveCurrencyId = x.ReceiveCurrencyId,
                SendCurrencyId = x.SendCurrencyId,
                ServiceLevels = x.ServiceLevels,
                IsFeeAdded = x.IsFeeAdded,
                Status = x.Status
            }).ToList();

            if (data == null || data.Count == 0)
            {
                response.Message = "merchant product fetch failed";
                return response;
            }

            response.Success = true;
            response.Message = "merchant product record fetched successfully";
            response.Data = data;
            return response;
        }

        public async Task<ApiResponse<MerchantProductViewModel>> UpdateMerchantProduct(UpdateMerchantProductRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = "Merchant Product Update Failed"
                    };

                var isAuthenticatedMerchant = await _merchantPermissionService.CheckMerchantPermission(model.MerchantId);
                if (!isAuthenticatedMerchant)
                {
                    return new ApiResponse<MerchantProductViewModel>() { Success = false, Message = "Invalid/Unauthorized merchant" };

                }

                var instrument = await _unitOfWork.GetRepository<Instrument>().GetById(model.PayoutInstrumentId);
                if (instrument == null || instrument.InstrumentType.Trim().ToLower() != "payout")
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = "Invalid instrument type. instrument must be payout"
                    };
                }
                var merchant = await _unitOfWork.GetRepository<Merchant>().GetById(model.MerchantId);

                if (merchant == null)
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = $"merchant with id {model.MerchantId} not found "
                    };
                }

                if (merchant.Country3Iso.Trim().ToLower() != model.SendCountry3Iso.Trim().ToLower())
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = $"merchant country not matches with send country."
                    };
                }

                var checkMerchantProductExist = await _unitOfWork.GetRepository<Merchant>().GetById(model.Id);
                if (checkMerchantProductExist == null)
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = "Merchant Product Update Failed"
                    };
                }

                var isRecordExist = await DoesRecordExistAsync(model.ServiceCategoryId, model.PayoutInstrumentId, model.ProductId, model.MerchantId, model.SendCountry3Iso, model.ReceiveCountry3Iso, model.SendCurrencyId, model.ReceiveCurrencyId);

                if (isRecordExist)
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = $"merchant product already exist with these details."
                    };
                }

                var updatedData = _mapper.Map<MerchantProduct>(model);
                await _unitOfWork.GetRepository<MerchantProduct>().Upsert(updatedData);
                await _unitOfWork.SaveChangesAsync();
                var responseData = await _unitOfWork.GetRepository<MerchantProduct>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(updatedData.Id);

                var merchantResponseData = new MerchantProductViewModel
                {
                    Id = responseData.Id,
                    MerchantId = responseData.MerchantId,
                    ServiceCategoryId = responseData.ServiceCategoryId,
                    ServiceCategoryName = responseData.ServiceCategory.ServCategoryName,
                    PayoutInstrumentId = responseData.InstrumentId,
                    PayoutInstrumentName = responseData.Instrument.InstrumentName,
                    ProductId = responseData.ProductId,
                    ProductName = responseData.Product.ProductName,
                    MerchantName = responseData.Merchant.MerchantName,
                    ReceiveCountry3Iso = responseData.ReceiveCountry3Iso,
                    SendCountry3Iso = responseData.SendCountry3Iso,
                    ReceiveCurrencyId = responseData.ReceiveCurrencyId,
                    SendCurrencyId = responseData.SendCurrencyId,
                    ServiceLevels = responseData.ServiceLevels,
                    IsFeeAdded = responseData.IsFeeAdded,
                    Status = responseData.Status
                };


                var response = new ApiResponse<MerchantProductViewModel>()
                {
                    Success = true,
                    Message = "Merchant Product record updated Successfully",
                    Data = merchantResponseData

                };
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("merchant product update failed");
            }
        }
        private async Task<bool> DoesRecordExistAsync(
                                        int serviceCategoryId,
                                        int instrumentId,
                                        int productId,
                                        int merchantId,
                                        string sendCountry3Iso,
                                        string receiveCountry3Iso,
                                        string sendCurrencyId,
                                        string receiveCurrencyId)
        {

            var merchantProducts = await _unitOfWork.GetRepository<MerchantProduct>().GetAll();
            return merchantProducts.Any(x => x.ServiceCategoryId == serviceCategoryId &&
                               x.InstrumentId == instrumentId &&
                               x.ProductId == productId &&
                               x.MerchantId == merchantId &&
                               x.SendCountry3Iso == sendCountry3Iso &&
                               x.ReceiveCountry3Iso == receiveCountry3Iso &&
                               x.SendCurrencyId == sendCurrencyId &&
                               x.ReceiveCurrencyId == receiveCurrencyId);
        }
    }
}
