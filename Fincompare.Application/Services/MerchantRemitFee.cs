using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantProductResponse;
using Fincompare.Domain.Entities;
using System.Diagnostics.Metrics;
using static Fincompare.Application.Request.MerchantRemitProductFeeRequests.MerchantRemitProductFeeRequestViewModel;
using static Fincompare.Application.Response.MerchantRemitFeeResponse.MerchantRemitFeeBaseResponse;

namespace Fincompare.Application.Services
{
    public class MerchantRemitFee : IMerchantRemitFee
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MerchantRemitFee(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<MerchantRemittanceFee>> AddMerchantRemitFee(CreateMerchantRemitProductFeeRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantRemittanceFee>() { Status = false, Message = "Request are UnVailed !" };

                var requestData =  _mapper.Map<MerchantRemitProductFee>(model);

                await _unitOfWork.GetRepository<MerchantRemitProductFee>().Add(requestData);
                await _unitOfWork.SaveChangesAsync();

                var merchantRemitFee =  await _unitOfWork.GetRepository<MerchantRemitProductFee>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(requestData.Id);

                var data = new MerchantRemittanceFee
                {
                    RemittanceFeeID = merchantRemitFee.Id,
                    MerchantID = merchantRemitFee.MerchantId,
                    MerchantName = merchantRemitFee.Merchant.MerchantName,
                    ServiceCategoryId = merchantRemitFee.MerchantProduct.ServiceCategoryId,
                    ServiceCategoryName = merchantRemitFee.MerchantProduct.ServiceCategory.ServCategoryName,
                    InstrumentId = merchantRemitFee.MerchantProduct.Instrument.Id,
                    InstrumentName = merchantRemitFee.MerchantProduct.Instrument.InstrumentName,
                    ProductId = merchantRemitFee.MerchantProduct.ProductId,
                    ProductName = merchantRemitFee.MerchantProduct.Product.ProductName,
                    FeesName = merchantRemitFee.FeesName,
                    FeesCurrency = merchantRemitFee.FeesCur,
                    Fees = merchantRemitFee.Fees,
                    PromoFees = merchantRemitFee.PromoFees,
                    MerchantProductID = merchantRemitFee.MerchantProductId,
                    SendCountry = merchantRemitFee.SendCountry3Iso,
                    ReceiveCountry = merchantRemitFee.ReceiveCurrency,
                    SendCurrency = merchantRemitFee.SendCurrency,
                    ReceiveCurrency = merchantRemitFee.ReceiveCurrency,
                    SendMinLimit = merchantRemitFee.SendMinLimit,
                    SendMaxLimit = merchantRemitFee.SendMinLimit,
                    ReceiveMinLimit = merchantRemitFee.ReceiveMinLimit,
                    ReceiveMaxLimit = merchantRemitFee.ReceiveMaxLimit,
                    ValidityExpiry = merchantRemitFee.ValidityExpiry,
                };

                return new ApiResponse<MerchantRemittanceFee>() { Status = true, Message = "Merchant Remittance Fee created successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        //public Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>> GetMerchantRemittanceFee(string sendCountry, string receiveCountry, string sendCurrency, string receiveCurrency, int? merchantId, int? remittanceFeeId, int? merchantProductId, int? serviceCategoryId, int? instrumentId, double? sendMinLimit, double? receiveMinLimit, bool? status, bool? isValid)
        //{
        //    var response = new ApiResponse<IEnumerable<MerchantProductViewModel>>();
        //    var merchantProducts = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();


        //    // Apply filters
        //    if (merchantId.HasValue)
        //        merchantProducts = merchantProducts.Where(mp => mp.MerchantId == merchantID.Value);
        //    if (merchantProductId.HasValue)
        //        merchantProducts = merchantProducts.Where(mp => mp.Id == merchantProductID.Value);
        //    if (productId.HasValue)
        //        merchantProducts = merchantProducts.Where(mp => mp.ProductId == productID.Value);
        //    if (serviceCategoryId.HasValue)
        //        merchantProducts = merchantProducts.Where(mp => mp.ServiceCategoryId == serviceCategoryID.Value);
        //    if (instrumentId.HasValue)
        //        merchantProducts = merchantProducts.Where(mp => mp.InstrumentId == instrumentID.Value);
        //    if (status.HasValue)
        //        merchantProducts = merchantProducts.Where(mp => mp.Status == status.Value);

        //    // Filter by required path parameters
        //    merchantProducts = merchantProducts
        //        .Where(mp => mp.SendCountry3Iso == sendCountry)
        //        .Where(mp => mp.ReceiveCountry3Iso == receiveCountry)
        //        .Where(mp => mp.SendCurrencyId == sendCurrency)
        //        .Where(mp => mp.ReceiveCurrencyId == receiveCurrency);

        //    if (!merchantProducts.Any())
        //    {
        //        response.Message = "merchant products not found";
        //        return response;
        //    }
        //    var data = merchantProducts.Select(x => new MerchantProductViewModel
        //    {
        //        Id = x.Id,
        //        MerchantId = x.MerchantId,
        //        ServiceCategoryId = x.ServiceCategoryId,
        //        ServiceCategoryName = x.ServiceCategory.ServCategoryName,
        //        InstrumentId = x.InstrumentId,
        //        InstrumentName = x.Instrument.InstrumentName,
        //        ProductId = x.ProductId,
        //        ProductName = x.Product.ProductName,
        //        MerchantName = x.Merchant.MerchantName,
        //        ReceiveCountry3Iso = x.ReceiveCountry3Iso,
        //        SendCountry3Iso = x.SendCountry3Iso,
        //        ReceiveCurrencyId = x.ReceiveCurrencyId,
        //        SendCurrencyId = x.SendCurrencyId,
        //        ServiceLevels = x.ServiceLevels,
        //        Status = x.Status
        //    }).ToList();

        //    response.Status = true;
        //    response.Message = "Merchant Products found";
        //    response.Data = data;
        //    return response;
        //}

        public async Task<ApiResponse<MerchantRemittanceFee>> UpdateMerchantRemitFee(UpdateMerchantRemitProductFeeRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantRemittanceFee>() { Status = false, Message = "Request are UnVailed !" };
                var checkData = await _unitOfWork.GetRepository<MerchantRemitProductFee>().GetById(model.Id);
                if (checkData == null)
                    return new ApiResponse<MerchantRemittanceFee>() { Status = false, Message = "Merchant Remittance Fee Not Found!" };

                var requestData = _mapper.Map(model, checkData);
                await _unitOfWork.GetRepository<MerchantRemitProductFee>().Upsert(requestData);
                await _unitOfWork.SaveChangesAsync();
                var merchantRemitFee = await _unitOfWork.GetRepository<MerchantRemitProductFee>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(requestData.Id);

                var data = new MerchantRemittanceFee
                {
                    RemittanceFeeID = merchantRemitFee.Id,
                    MerchantID = merchantRemitFee.MerchantId,
                    MerchantName = merchantRemitFee.Merchant.MerchantName,
                    ServiceCategoryId = merchantRemitFee.MerchantProduct.ServiceCategoryId,
                    ServiceCategoryName = merchantRemitFee.MerchantProduct.ServiceCategory.ServCategoryName,
                    InstrumentId = merchantRemitFee.MerchantProduct.Instrument.Id,
                    InstrumentName = merchantRemitFee.MerchantProduct.Instrument.InstrumentName,
                    ProductId = merchantRemitFee.MerchantProduct.ProductId,
                    ProductName = merchantRemitFee.MerchantProduct.Product.ProductName,
                    FeesName = merchantRemitFee.FeesName,
                    FeesCurrency = merchantRemitFee.FeesCur,
                    Fees = merchantRemitFee.Fees,
                    PromoFees = merchantRemitFee.PromoFees,
                    MerchantProductID = merchantRemitFee.MerchantProductId,
                    SendCountry = merchantRemitFee.SendCountry3Iso,
                    ReceiveCountry = merchantRemitFee.ReceiveCurrency,
                    SendCurrency = merchantRemitFee.SendCurrency,
                    ReceiveCurrency = merchantRemitFee.ReceiveCurrency,
                    SendMinLimit = merchantRemitFee.SendMinLimit,
                    SendMaxLimit = merchantRemitFee.SendMinLimit,
                    ReceiveMinLimit = merchantRemitFee.ReceiveMinLimit,
                    ReceiveMaxLimit = merchantRemitFee.ReceiveMaxLimit,
                    ValidityExpiry = merchantRemitFee.ValidityExpiry,
                    
                };

                return new ApiResponse<MerchantRemittanceFee>() { Status = true, Message = "Merchant Remittance Fee updated successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }



    }
}
