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
                    return new ApiResponse<string>() { Status = false, Message = "Coupon usage failed" };
                var usedCoupon = _mapper.Map<CustomerUsedCoupon>(model);
                await _unitOfWork.GetRepository<CustomerUsedCoupon>().Add(usedCoupon);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Status = true, Message = "Coupon captured successfully" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);

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

                var getAllMerchantProduct = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();

                //var joinData = from cus in getAllCustomerUsed
                //               join mp in getAllMerchantProduct
                //               on cus.Merchant.Id equals mp.MerchantId
                //               select cus;



                if (merchantId.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.MerchantId == merchantId);
                if (customerId.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.CustomerUserId == customerId);
                if (merchantProductID.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.MerchantProductCoupon.MerchantProductId == merchantProductID);
                if(isUsed.HasValue)
                    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.IsUsed == isUsed);

                //if (startDateTime.HasValue)
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.CustomerUserId == customerId);
                //if (endDateTime.HasValue)
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.CustomerUserId == customerId);
                //if (!string.IsNullOrEmpty(sendCountry))
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x. == customerId);
                //if (!string.IsNullOrEmpty(receiveCountry))
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.Merchant. == customerId);
                //if (!string.IsNullOrEmpty(sendCurrency))
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.Merchant.MerchantProductCoupons == customerId);
                //if (!string.IsNullOrEmpty(receiveCurrency))
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.Merchant. == customerId);
                //if (serviceCategoryId.HasValue)
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.mer == customerId);
                //if (instrumentId.HasValue)
                //    getAllCustomerUsed = getAllCustomerUsed.Where(x => x.Merchant. == customerId);


                var response = _mapper.Map<IEnumerable<GetAllCustomerUsedCouponResponse>>(getAllCustomerUsed);

                if (getAllCustomerUsed.ToList().Count > 0)
                    return new ApiResponse<IEnumerable<GetAllCustomerUsedCouponResponse>>() { Status = true, Message = "Fetch All Used Coupons Successfully!", Data = response };
                return new ApiResponse<IEnumerable<GetAllCustomerUsedCouponResponse>>() { Status = true, Message = "Used Coupons Fetch Failed!" };

            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);

            }
        }

    }
}
