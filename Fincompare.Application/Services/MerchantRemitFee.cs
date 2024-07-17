using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
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
        public async Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>> AddMerchantRemitFee(CreateMerchantRemitProductFeeRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Status = false, Message = "Request are UnVailed !" };

                var requestData =  _mapper.Map<MerchantRemitProductFee>(model);
                var merchantProducts =  _unitOfWork.GetRepository<MerchantRemitProductFee>().GetAllRelatedEntity().AsQueryable();

                var data = merchantProducts.Select(x => new MerchantRemittanceFee
                {
                    RemittanceFeeID = x.Id,
                    MerchantID = x.MerchantId,
                    MerchantName = x.Merchant.MerchantName,
                    ServiceCategoryId = x.MerchantProduct.ServiceCategoryId,
                    ServiceCategoryName = x.MerchantProduct.ServiceCategory.ServCategoryName,
                    InstrumentId = x.MerchantProduct.Instrument.Id,
                    InstrumentName = x.MerchantProduct.Instrument.InstrumentName,
                    ProductId = x.MerchantProduct.ProductId,
                    ProductName = x.MerchantProduct.Product.ProductName,
                    FeesName = x.FeesName,
                    FeesCurrency = x.FeesCur,
                    Fees = x.Fees,
                    PromoFees = x.PromoFees,
                    MerchantProductID = x.MerchantProductId,
                    SendCountry = x.SendCountry3Iso,
                    ReceiveCountry = x.ReceiveCurrency,
                    SendCurrency = x.SendCurrency,
                    ReceiveCurrency = x.ReceiveCurrency,
                    SendMinLimit = x.SendMinLimit,
                    SendMaxLimit = x.SendMinLimit,
                    ReceiveMinLimit = x.ReceiveMinLimit,
                    ReceiveMaxLimit = x.ReceiveMaxLimit,
                    ValidityExpiry = x.ValidityExpiry,
                }).ToList();

                return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Status = true, Message = "Merchant Remittance Fee created successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>> UpdateMerchantRemitFee(UpdateMerchantRemitProductFeeRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Status = false, Message = "Request are UnVailed !" };
                var checkData = await _unitOfWork.GetRepository<MerchantRemitProductFee>().GetById(model.RemittanceFeeID);
                if (checkData == null)
                    return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Status = false, Message = "Merchant Remittance Fee Not Found!" };

                var requestData = _mapper.Map(model, checkData);
                var merchantProducts = _unitOfWork.GetRepository<MerchantRemitProductFee>().GetAllRelatedEntity().AsQueryable();

                var data = merchantProducts.Select(x => new MerchantRemittanceFee
                {
                    RemittanceFeeID = x.Id,
                    MerchantID = x.MerchantId,
                    MerchantName = x.Merchant.MerchantName,
                    ServiceCategoryId = x.MerchantProduct.ServiceCategoryId,
                    ServiceCategoryName = x.MerchantProduct.ServiceCategory.ServCategoryName,
                    InstrumentId = x.MerchantProduct.Instrument.Id,
                    InstrumentName = x.MerchantProduct.Instrument.InstrumentName,
                    ProductId = x.MerchantProduct.ProductId,
                    ProductName = x.MerchantProduct.Product.ProductName,
                    FeesName = x.FeesName,
                    FeesCurrency = x.FeesCur,
                    Fees = x.Fees,
                    PromoFees = x.PromoFees,
                    MerchantProductID = x.MerchantProductId,
                    SendCountry = x.SendCountry3Iso,
                    ReceiveCountry = x.ReceiveCurrency,
                    SendCurrency = x.SendCurrency,
                    ReceiveCurrency = x.ReceiveCurrency,
                    SendMinLimit = x.SendMinLimit,
                    SendMaxLimit = x.SendMinLimit,
                    ReceiveMinLimit = x.ReceiveMinLimit,
                    ReceiveMaxLimit = x.ReceiveMaxLimit,
                    ValidityExpiry = x.ValidityExpiry,
                }).ToList();

                return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Status = true, Message = "Merchant Remittance Fee updated successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }



    }
}
