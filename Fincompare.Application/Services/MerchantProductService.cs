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

        public MerchantProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<MerchantProductViewModel>> AddMerchantProduct(AddMerchantProductRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = true,
                        Message = "merchant product creation failed"
                    };
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
                    InstrumentId = responseData.InstrumentId,
                    InstrumentName = responseData.Instrument.InstrumentName,
                    ProductId = responseData.ProductId,
                    ProductName = responseData.Product.ProductName,
                    MerchantName = responseData.Merchant.MerchantName,
                    ReceiveCountry3Iso = responseData.ReceiveCountry3Iso,
                    SendCountry3Iso = responseData.SendCountry3Iso,
                    ReceiveCurrencyId = responseData.ReceiveCurrencyId,
                    SendCurrencyId = responseData.SendCurrencyId,
                    ServiceLevels = responseData.ServiceLevels,
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
                                                    InstrumentId = x.InstrumentId,
                                                    InstrumentName = x.Instrument.InstrumentName,
                                                    ProductId = x.ProductId,
                                                    ProductName = x.Product.ProductName,
                                                    MerchantName = x.Merchant.MerchantName,
                                                    ReceiveCountry3Iso = x.ReceiveCountry3Iso,
                                                    SendCountry3Iso = x.SendCountry3Iso,
                                                    ReceiveCurrencyId = x.ReceiveCurrencyId,
                                                    SendCurrencyId = x.SendCurrencyId,
                                                    ServiceLevels = x.ServiceLevels,
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
                InstrumentId = x.InstrumentId,
                InstrumentName = x.Instrument.InstrumentName,
                ProductId = x.ProductId,
                ProductName = x.Product.ProductName,
                MerchantName = x.Merchant.MerchantName,
                ReceiveCountry3Iso = x.ReceiveCountry3Iso,
                SendCountry3Iso = x.SendCountry3Iso,
                ReceiveCurrencyId = x.ReceiveCurrencyId,
                SendCurrencyId = x.SendCurrencyId,
                ServiceLevels = x.ServiceLevels,
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
                var checkMerchantProductExist = await _unitOfWork.GetRepository<Merchant>().GetById(model.Id);
                if (checkMerchantProductExist == null)
                {
                    return new ApiResponse<MerchantProductViewModel>()
                    {
                        Success = false,
                        Message = "Merchant Product Update Failed"
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
                    InstrumentId = responseData.InstrumentId,
                    InstrumentName = responseData.Instrument.InstrumentName,
                    ProductId = responseData.ProductId,
                    ProductName = responseData.Product.ProductName,
                    MerchantName = responseData.Merchant.MerchantName,
                    ReceiveCountry3Iso = responseData.ReceiveCountry3Iso,
                    SendCountry3Iso = responseData.SendCountry3Iso,
                    ReceiveCurrencyId = responseData.ReceiveCurrencyId,
                    SendCurrencyId = responseData.SendCurrencyId,
                    ServiceLevels = responseData.ServiceLevels,
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
    }
}
