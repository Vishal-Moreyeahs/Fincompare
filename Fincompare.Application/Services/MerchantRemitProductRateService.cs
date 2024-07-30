using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantRemitProductRateRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantRemitProductRateResponse;
using Fincompare.Domain.Entities;
namespace Fincompare.Application.Services
{
    public class MerchantRemitProductRateService : IMerchantRemitProductRateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MerchantRemitProductRateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<MerchantRemitProductRateViewModel>> AddMerchantRemitProductRate(AddMerchantRemitProductRateRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantRemitProductRateViewModel>() { Success = false, Message = "merchant product remmitance rate creation failed" };

                var requestData = _mapper.Map<MerchantRemitProductRate>(model);

                await _unitOfWork.GetRepository<MerchantRemitProductRate>().Add(requestData);
                await _unitOfWork.SaveChangesAsync();

                var merchantProduct = new MerchantProduct();
                var merchantRemitRate = await _unitOfWork.GetRepository<MerchantRemitProductRate>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(requestData.Id);
                if (merchantRemitRate.MerchantProductId.HasValue)
                    merchantProduct = await _unitOfWork.GetRepository<MerchantProduct>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(merchantRemitRate.MerchantProductId.Value);

                var data = new MerchantRemitProductRateViewModel
                {
                    RemittanceRateId = merchantRemitRate.Id,
                    MerchantRateRef = merchantRemitRate.MerchantRateRef,
                    MerchantId = merchantRemitRate.MerchantId,
                    MerchantName = merchantRemitRate.Merchant.MerchantName,
                    MerchantProductId = merchantRemitRate.MerchantProductId.HasValue ? merchantRemitRate.MerchantProductId.Value : null,
                    ProductId = merchantProduct.ProductId,
                    ProductName = merchantProduct == null ? "" : merchantProduct.Product.ProductName,
                    InstrumentId = merchantProduct.InstrumentId,
                    InstrumentName = merchantProduct == null ? "" : merchantProduct.Instrument.InstrumentName,
                    ServiceCategoryId = merchantProduct.ServiceCategoryId,
                    ServiceCategoryName = merchantProduct == null ? "" : merchantProduct.ServiceCategory.ServCategoryName,
                    SendCountry3Iso = merchantRemitRate.SendCountry3Iso,
                    ReceiveCountry3Iso = merchantRemitRate.ReceiveCountry3Iso,
                    SendCur = merchantRemitRate.SendCur,
                    ReceiveCur = merchantRemitRate.ReceiveCur,
                    SendMinLimit = merchantRemitRate.SendMinLimit,
                    SendMaxLimit = merchantRemitRate.SendMaxLimit,
                    ReceiveMinLimit = merchantRemitRate.ReceiveMinLimit,
                    ReceiveMaxLimit = merchantRemitRate.ReceiveMaxLimit,
                    Rate = merchantRemitRate.Rate,
                    PromoRate = merchantRemitRate.PromoRate,
                    ValidityExpiry = merchantRemitRate.ValidityExpiry,
                    Status = merchantProduct.Status
                };

                return new ApiResponse<MerchantRemitProductRateViewModel>() { Success = true, Message = "Merchant Remittance Product Rate record created successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException($"merchant product remmitance rate creation failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>> GetAllMerchantRemitProductRate(string sendCountry, string receiveCountry, string sendCurrency, string receiveCurrency, int? merchantId, int? remittanceRateId, int? merchantProductId, int? serviceCategoryId, int? instrumentId, double? sendMinLimit, double? receiveMinLimit, bool? status)
        {
            try
            {
                //var response = new ApiResponse<IEnumerable<MerchantRemittanceFee>>();
                var merchantRemitRates = _unitOfWork.GetRepository<MerchantRemitProductRate>().GetAllRelatedEntity().AsQueryable();

                var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();
                // Apply filters
                //if (merchantId.HasValue)
                //    merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantId == merchantId.Value);
                if (merchantProductId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.MerchantProductId == merchantProductId.Value);
                if (merchantId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.MerchantId == merchantId.Value);
                if (remittanceRateId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.Id == remittanceRateId.Value);
                if (serviceCategoryId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.MerchantProduct.ServiceCategoryId == serviceCategoryId.Value);
                if (instrumentId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.MerchantProduct.InstrumentId == instrumentId.Value);
                if (sendMinLimit.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.SendMinLimit >= sendMinLimit.Value);
                if (receiveMinLimit.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.ReceiveMinLimit <= receiveMinLimit.Value);
                if (status.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.Status == status.Value);
                //if (isValid.HasValue)
                //    merchantRemitFees = merchantRemitFees.Where(mp => mp.va == status.Value);

                // Filter by required path parameters
                merchantRemitRates = merchantRemitRates
                    .Where(mp => mp.SendCountry3Iso == sendCountry)
                    .Where(mp => mp.ReceiveCountry3Iso == receiveCountry)
                    .Where(mp => mp.SendCur == sendCurrency)
                    .Where(mp => mp.ReceiveCur == receiveCurrency);

                var innerData = (from merchantRemitRate in merchantRemitRates
                                 join mp in merchantProductList
                                 on merchantRemitRate.MerchantProductId equals mp.Id
                                 select new MerchantRemitProductRateViewModel
                                 {
                                     RemittanceRateId = merchantRemitRate.Id,
                                     MerchantId = merchantRemitRate.MerchantId,
                                     MerchantName = merchantRemitRate.Merchant.MerchantName,
                                     MerchantRateRef = merchantRemitRate.MerchantRateRef,
                                     ServiceCategoryId = merchantRemitRate.MerchantProduct.ServiceCategoryId,
                                     ServiceCategoryName = mp.ServiceCategory.ServCategoryName,
                                     InstrumentId = mp.Instrument.Id,
                                     InstrumentName = mp.Instrument.InstrumentName,
                                     ProductId = merchantRemitRate.MerchantProduct.ProductId,
                                     ProductName = mp.Product.ProductName,
                                     Rate = merchantRemitRate.Rate,
                                     PromoRate = merchantRemitRate.PromoRate,
                                     MerchantProductId = merchantRemitRate.MerchantProductId,
                                     SendCountry3Iso = merchantRemitRate.SendCountry3Iso,
                                     ReceiveCountry3Iso = merchantRemitRate.ReceiveCountry3Iso,
                                     SendCur = merchantRemitRate.SendCur,
                                     ReceiveCur = merchantRemitRate.ReceiveCur,
                                     SendMinLimit = merchantRemitRate.SendMinLimit,
                                     SendMaxLimit = merchantRemitRate.SendMaxLimit,
                                     ReceiveMinLimit = merchantRemitRate.ReceiveMinLimit,
                                     ReceiveMaxLimit = merchantRemitRate.ReceiveMaxLimit,
                                     ValidityExpiry = merchantRemitRate.ValidityExpiry
                                 }).ToList();


                if (innerData.Count > 0)
                    return new ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>() { Success = true, Message = "merchant product remmitance rate record fetched successfully", Data = innerData };
                return new ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>() { Success = false, Message = "merchant product remmitance rate fetch failed" };

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"merchant product remmitance rate fetch failed {ex.Message}");
            }

        }

        public async Task<ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>> GetAllMerchantRemitProductRateByMerchant(int merchantId)
        {
            try
            {
                var merchantRemitRates = _unitOfWork.GetRepository<MerchantRemitProductRate>().GetAllRelatedEntity().AsQueryable();

                var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();


                // Filter by required path parameters
                merchantRemitRates = merchantRemitRates.Where(mp => mp.MerchantId == merchantId);

                var innerData = (from merchantRemitRate in merchantRemitRates
                                 join mp in merchantProductList
                                 on merchantRemitRate.MerchantProductId equals mp.Id
                                 select new MerchantRemitProductRateViewModel
                                 {
                                     RemittanceRateId = merchantRemitRate.Id,
                                     MerchantId = merchantRemitRate.MerchantId,
                                     MerchantName = merchantRemitRate.Merchant.MerchantName,
                                     MerchantRateRef = merchantRemitRate.MerchantRateRef,
                                     ServiceCategoryId = merchantRemitRate.MerchantProduct.ServiceCategoryId,
                                     ServiceCategoryName = mp.ServiceCategory.ServCategoryName,
                                     InstrumentId = mp.Instrument.Id,
                                     InstrumentName = mp.Instrument.InstrumentName,
                                     ProductId = merchantRemitRate.MerchantProduct.ProductId,
                                     ProductName = mp.Product.ProductName,
                                     Rate = merchantRemitRate.Rate,
                                     PromoRate = merchantRemitRate.PromoRate,
                                     MerchantProductId = merchantRemitRate.MerchantProductId,
                                     SendCountry3Iso = merchantRemitRate.SendCountry3Iso,
                                     ReceiveCountry3Iso = merchantRemitRate.ReceiveCountry3Iso,
                                     SendCur = merchantRemitRate.SendCur,
                                     ReceiveCur = merchantRemitRate.ReceiveCur,
                                     SendMinLimit = merchantRemitRate.SendMinLimit,
                                     SendMaxLimit = merchantRemitRate.SendMaxLimit,
                                     ReceiveMinLimit = merchantRemitRate.ReceiveMinLimit,
                                     ReceiveMaxLimit = merchantRemitRate.ReceiveMaxLimit,
                                     ValidityExpiry = merchantRemitRate.ValidityExpiry
                                 }).ToList();


                if (innerData.Count > 0)
                    return new ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>() { Success = true, Message = "merchant product remmitance rate record fetched successfully", Data = innerData };
                return new ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>() { Success = false, Message = "merchant product remmitance rate fetch failed" };

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"merchant product remmitance rate fetch failed {ex.Message}");
            }

        }

        public async Task<ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>> GetAllMerchantRemitProductRateByCurrencyPairAndMerchant(string sendCurrency, string receiveCurrency, int merchantId, int? remittanceRateId, int? merchantProductId, int? serviceCategoryId, int? instrumentId, double? sendMinLimit, double? receiveMinLimit, bool? status)
        {
            try
            {
                var merchantRemitRates = _unitOfWork.GetRepository<MerchantRemitProductRate>().GetAllRelatedEntity().AsQueryable();

                var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();
                // Apply filters
                //if (merchantId.HasValue)
                //    merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantId == merchantId.Value);
                if (merchantProductId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.MerchantProductId == merchantProductId.Value);
                if (remittanceRateId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.Id == remittanceRateId.Value);
                if (serviceCategoryId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.MerchantProduct.ServiceCategoryId == serviceCategoryId.Value);
                if (instrumentId.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.MerchantProduct.InstrumentId == instrumentId.Value);
                if (sendMinLimit.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.SendMinLimit == sendMinLimit.Value);
                if (receiveMinLimit.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.ReceiveMinLimit == receiveMinLimit.Value);
                if (status.HasValue)
                    merchantRemitRates = merchantRemitRates.Where(mp => mp.Status == status.Value);
                //if (isValid.HasValue)
                //    merchantRemitFees = merchantRemitFees.Where(mp => mp.va == status.Value);

                // Filter by required path parameters
                merchantRemitRates = merchantRemitRates
                    .Where(mp => mp.SendCur == sendCurrency)
                    .Where(mp => mp.ReceiveCur == receiveCurrency)
                    .Where(mp => mp.MerchantId == merchantId);

                var innerData = (from merchantRemitRate in merchantRemitRates
                                 join mp in merchantProductList
                                 on merchantRemitRate.MerchantProductId equals mp.Id
                                 select new MerchantRemitProductRateViewModel
                                 {
                                     RemittanceRateId = merchantRemitRate.Id,
                                     MerchantId = merchantRemitRate.MerchantId,
                                     MerchantName = merchantRemitRate.Merchant.MerchantName,
                                     MerchantRateRef = merchantRemitRate.MerchantRateRef,
                                     ServiceCategoryId = merchantRemitRate.MerchantProduct.ServiceCategoryId,
                                     ServiceCategoryName = mp.ServiceCategory.ServCategoryName,
                                     InstrumentId = mp.Instrument.Id,
                                     InstrumentName = mp.Instrument.InstrumentName,
                                     ProductId = merchantRemitRate.MerchantProduct.ProductId,
                                     ProductName = mp.Product.ProductName,
                                     Rate = merchantRemitRate.Rate,
                                     PromoRate = merchantRemitRate.PromoRate,
                                     MerchantProductId = merchantRemitRate.MerchantProductId,
                                     SendCountry3Iso = merchantRemitRate.SendCountry3Iso,
                                     ReceiveCountry3Iso = merchantRemitRate.ReceiveCountry3Iso,
                                     SendCur = merchantRemitRate.SendCur,
                                     ReceiveCur = merchantRemitRate.ReceiveCur,
                                     SendMinLimit = merchantRemitRate.SendMinLimit,
                                     SendMaxLimit = merchantRemitRate.SendMaxLimit,
                                     ReceiveMinLimit = merchantRemitRate.ReceiveMinLimit,
                                     ReceiveMaxLimit = merchantRemitRate.ReceiveMaxLimit,
                                     ValidityExpiry = merchantRemitRate.ValidityExpiry
                                 }).ToList();


                if (innerData.Count > 0)
                    return new ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>() { Success = true, Message = "merchant product remmitance rate record fetched successfully", Data = innerData };
                return new ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>() { Success = false, Message = "merchant product remmitance rate fetch failed" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"merchant product remmitance rate fetch failed {ex.Message}");
            }

        }
    }
}
