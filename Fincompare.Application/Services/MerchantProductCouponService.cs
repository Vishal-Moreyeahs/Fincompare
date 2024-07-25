using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.MerchantProductCouponRequest.MerchantProductRequestViewModel;
using static Fincompare.Application.Response.MerchantProductCouponResponse.MerchantProductCouponViewResponse;

namespace Fincompare.Application.Services
{
    public class MerchantProductCouponService : IMerchantProductCouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MerchantProductCouponService(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _unitOfWork = iUnitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetAllMerchantProductCouponResponse>>> CreateMerchantProductCoupons(List<CreateMerchantProductCouponRequest> model)
        {
            try
            {
                if (model.Count == 0)
                    return new ApiResponse<IEnumerable<GetAllMerchantProductCouponResponse>>() { Success = false, Message = "Merchant product coupon creation failed" };

                var addProductCouponse = _mapper.Map<IEnumerable<MerchantProductCoupon>>(model);

                await _unitOfWork.GetRepository<MerchantProductCoupon>().AddRange(addProductCouponse);
                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<IEnumerable<GetAllMerchantProductCouponResponse>>(addProductCouponse);
                return new ApiResponse<IEnumerable<GetAllMerchantProductCouponResponse>>() { Success = true, Message = "Merchant product coupon created successfully", Data = response };
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);

            }
        }

        public async Task<ApiResponse<IEnumerable<MerchantCouponResponseClass>>> GetAllMerchantProductCoupons
            (
            int merchantId,
            string? merchantCouponBatch,
            int? merchantProductId,
            int? serviceCategoryId,
            int? instrumentId,
            int? productId,
            string? sendCountry,
            string? receiveCountry,
            string? sendCurrency,
            string? receiveCurrency,
            bool? IsUsed,
            bool? status

            )
        {
            try
            {
                var merchantProductCoupons = _unitOfWork.GetRepository<MerchantProductCoupon>().GetAllRelatedEntity().AsQueryable();

                var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();
                // Apply filters

                if (merchantProductId.HasValue)
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantProductId == merchantProductId.Value);
                if (!string.IsNullOrEmpty(merchantCouponBatch))
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantCouponBatch == merchantCouponBatch);
                if (productId.HasValue)
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantProduct.ProductId == productId.Value);
                if (!string.IsNullOrEmpty(sendCountry))
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantProduct.SendCountry3Iso == sendCountry);
                if (serviceCategoryId.HasValue)
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantProduct.ServiceCategoryId == serviceCategoryId.Value);
                if (instrumentId.HasValue)
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantProduct.InstrumentId == instrumentId.Value);
                if (!string.IsNullOrEmpty(receiveCountry))
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantProduct.ReceiveCountry3Iso == receiveCountry);
                if (!string.IsNullOrEmpty(sendCurrency))
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantProduct.SendCurrencyId == sendCurrency);
                if (!string.IsNullOrEmpty(receiveCurrency))
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.MerchantProduct.ReceiveCurrencyId == receiveCurrency);
                if (IsUsed.HasValue)
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.IsUsed == IsUsed.Value);
                if (status.HasValue)
                    merchantProductCoupons = merchantProductCoupons.Where(mp => mp.Status == status.Value);
                // Filter by required path parameters
                merchantProductCoupons = merchantProductCoupons
                    .Where(mp => mp.MerchantProduct.MerchantId == merchantId);

                var innerData = (from merchantProduct in merchantProductCoupons
                                 join mp in merchantProductList
                                 on merchantProduct.MerchantProductId equals mp.Id
                                 select new MerchantCouponResponseClass
                                 {
                                     MerchantCouponId = merchantProduct.Id,
                                     CouponId = merchantProduct.CouponId,
                                     MerchantProductId = (int)merchantProduct.MerchantProductId,
                                     MerchantId = (int)merchantProduct.MerchantId,
                                     MerchantName = mp.Merchant.MerchantName,
                                     ServiceCategoryId = mp.Product.ServiceCategoryId,
                                     ServiceCategoryName = mp.ServiceCategory.ServCategoryName,
                                     InstrumentId = mp.InstrumentId,
                                     InstrumentName = mp.Instrument.InstrumentName,
                                     ProductId = mp.ProductId,
                                     ProductName = mp.Product.ProductName,
                                     MerchantCouponBatch = merchantProduct.MerchantCouponBatch,
                                     CouponCode = merchantProduct.CouponCode,
                                     IsMultiple = merchantProduct.IsMultiple,
                                     IsUsed = merchantProduct.IsUsed,
                                     SendCountry = merchantProduct.MerchantProduct.SendCountry3Iso,
                                     ReceiveCountry = merchantProduct.MerchantProduct.ReceiveCountry3Iso,
                                     SendCurrency = merchantProduct.MerchantProduct.SendCurrencyId,
                                     ReceiveCurrency = merchantProduct.MerchantProduct.ReceiveCurrencyId,
                                     ValidityFrom = merchantProduct.ValidityFrom,
                                     ValidityTo = merchantProduct.ValidityTo,
                                     Status = merchantProduct.Status,

                                 }).ToList();


                if (innerData.Count > 0)
                    return new ApiResponse<IEnumerable<MerchantCouponResponseClass>>() { Success = true, Message = "Merchant product coupon fetch successfully!", Data = innerData };
                return new ApiResponse<IEnumerable<MerchantCouponResponseClass>>() { Success = false, Message = "Merchant product coupon not found!" };
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ApiResponse<string>> UpdateMerchantProductCoupons(UpdateMerchantProductCouponRequest model)
        {
            try
            {
                var getAllUpdateMerchan = await _unitOfWork.GetRepository<MerchantProductCoupon>().GetAll();
                var checkMerchantProduct = getAllUpdateMerchan.Where(x => (x.MerchantId == model.MerchantId && x.MerchantCouponBatch == model.MerchantCouponBatch) || x.Id == model.MerchantCouponId).FirstOrDefault();
                if (checkMerchantProduct == null)
                    return new ApiResponse<string>() { Success = false, Message = "Merchant product coupon update failed" };
                var updateData = _mapper.Map<MerchantProductCoupon>(checkMerchantProduct);
                await _unitOfWork.GetRepository<MerchantProductCoupon>().Upsert(updateData);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Success = false, Message = "Merchant product coupon updated successfully" };

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

    }
}
