using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.CustomerUsedCouponRequest.CustomerUsedCouponViewRequest;
using static Fincompare.Application.Response.CustomerUsedCouponResponse.CustomerUsedCouponViewResponse;

namespace Fincompare.Application.Services
{
    public class CustomerUsedCouponService : ICustomerUsedCouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerUsedCouponService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> CustomerUsedCoupons(CreateCustomerUsedCouponRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<string>() { Success = false, Message = "customer used coupon creation failed" };
                var usedCoupon = _mapper.Map<CustomerUsedCoupon>(model);
                await _unitOfWork.GetRepository<CustomerUsedCoupon>().Add(usedCoupon);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Success = true, Message = "customer used coupon record created successfully" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"customer used coupon creation failed {ex.Message}");

            }


        }

        public async Task<ApiResponse<IEnumerable<GetAllCustomerUsedCouponResponse>>> GetAllCustomerUsedCoupons
            (
            int? merchantId,
            int? customerId,
            DateTime? startDateTime,
            DateTime? endDateTime,
            string? sendCountry,
            string? receiveCountry,
            string? sendCurrency,
            string? receiveCurrency,
            int? merchantProductID,
            int? serviceCategoryId,
            int? instrumentId,
            bool? isUsed
            )
        {
            try
            {
                var getAllCustomerUsed = _unitOfWork.GetRepository<CustomerUsedCoupon>().GetAllRelatedEntity().AsQueryable();

                if (merchantId.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.MerchantId == merchantId);
                if (customerId.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.CustomerUserId == customerId);
                if (merchantProductID.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.MerchantProductCoupon.MerchantProductId == merchantProductID);
                if (isUsed.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.IsUsed == isUsed);
                if (startDateTime.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.MerchantProductCoupon.ValidityFrom >= startDateTime);
                if (endDateTime.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.MerchantProductCoupon.ValidityTo <= endDateTime);

                var getAllMerchantProductCoupan = _unitOfWork.GetRepository<MerchantProductCoupon>().GetAllRelatedEntity().AsQueryable();
                var getAllMerchantProduct = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();

                var result = from customerUsed in getAllCustomerUsed
                             join merchantProductCoupon in getAllMerchantProductCoupan
                             on customerUsed.MerchantProductCouponId equals merchantProductCoupon.Id into custProdCoupGroup
                             from merchantProductCoupon in custProdCoupGroup.DefaultIfEmpty()
                             join merchantProduct in getAllMerchantProduct
                             on merchantProductCoupon.MerchantProductId equals merchantProduct.Id into prodGroup
                             from merchantProduct in prodGroup.DefaultIfEmpty()
                             select new ResponseView
                             {
                                 CustomerUsedCoupan = customerUsed,
                                 MerchantProductCoupon = merchantProductCoupon,
                                 MerchantProduct = merchantProduct
                             };

                if (!string.IsNullOrEmpty(sendCountry))
                    result = result.Where(x => x.MerchantProduct.SendCountry3Iso == sendCountry);
                if (!string.IsNullOrEmpty(receiveCountry))
                    result = result.Where(x => x.MerchantProduct.ReceiveCountry3Iso == receiveCountry);
                if (serviceCategoryId.HasValue)
                    result = result.Where(x => x.MerchantProduct.ServiceCategoryId == serviceCategoryId);
                if (instrumentId.HasValue)
                    result = result.Where(x => x.MerchantProduct.InstrumentId == instrumentId);
                //if (!string.IsNullOrEmpty(sendCurrency))
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.Merchant.MerchantProductCoupons == customerId);
                //if (!string.IsNullOrEmpty(receiveCurrency))
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.Merchant. == customerId);

                getAllCustomerUsed = result.Select(x => x.CustomerUsedCoupan);

                var response = _mapper.Map<IEnumerable<GetAllCustomerUsedCouponResponse>>(getAllCustomerUsed);

                if (getAllCustomerUsed.ToList().Count > 0)
                    return new ApiResponse<IEnumerable<GetAllCustomerUsedCouponResponse>>() { Success = true, Message = "customer used coupon record fetched successfully", Data = response };
                return new ApiResponse<IEnumerable<GetAllCustomerUsedCouponResponse>>() { Success = false, Message = "customer used coupon fetch failed" };

            }
            catch (Exception ex)
            {

                throw new ApplicationException($"customer used coupon fetch failed {ex.Message}");

            }
        }

        public class ResponseView
        {
            public CustomerUsedCoupon CustomerUsedCoupan { get; set; }
            public MerchantProductCoupon MerchantProductCoupon { get; set; }
            public MerchantProduct MerchantProduct { get; set; }
        }

    }
}
