using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request;
using Fincompare.Application.Response;
using Fincompare.Application.Response.ClickLeadResponse;
using Fincompare.Application.Response.CustomerReviewResponse;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Response.CouponResponse.CouponResponseViewModel;

namespace Fincompare.Application.Services
{
    public class CustomerReviewService : ICustomerReviewService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<CustomerReviewResponseViewModel>> AddCustomerReviewRecord(AddCustomerReviewRequest model)
        {

            try
            {
                if (model == null)
                    return new ApiResponse<CustomerReviewResponseViewModel>() { Success = false, Message = "customer review creation failed" };

                var addReview = _mapper.Map<CustomerReview>(model);
                await _unitOfWork.GetRepository<CustomerReview>().Add(addReview);
                await _unitOfWork.SaveChangesAsync();
                var data = await _unitOfWork.GetRepository<ClickLead>().GetByPrimaryKeyWithRelatedEntitiesAsync(addReview.Id);

                var response = _mapper.Map<CustomerReviewResponseViewModel>(data);
                response.MerchantName = data.Merchant.MerchantName;

                return new ApiResponse<CustomerReviewResponseViewModel>() { Success = true, Message = "Click lead created successfully", Data = response };

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<CustomerReviewResponseViewModel>>> GetAllResponse(int? merchantId)
        {
            try
            {
                var getAllCustomerReview = _unitOfWork.GetRepository<CustomerReview>().GetAllRelatedEntity();


                if (merchantId.HasValue)
                    getAllCustomerReview = getAllCustomerReview.Where(mp => mp.MerchantId == merchantId.Value);

                var getData = getAllCustomerReview
                    .Select(x => new CustomerReviewResponseViewModel
                    {
                        Id = x.Id,
                        Rating = x.Rating,
                        Review = x.Review,
                        Status = x.Status,
                        MerchantId = x.MerchantId,
                        MerchantName = x.Merchant.MerchantName
                    }).ToList();

                if (getData.Count == 0)
                    return new ApiResponse<IEnumerable<CustomerReviewResponseViewModel>>() { Success = false, Message = "Customer review records not found!" };
                return new ApiResponse<IEnumerable<CustomerReviewResponseViewModel>>() { Success = true, Message = "customer review records Found!", Data = getData };
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<ApiResponse<CustomerReviewResponseViewModel>> UpdateCustomerReviewRecord(UpdateCustomerReviewRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<CustomerReviewResponseViewModel>() { Success = false, Message = "customer review update  failed" };
                var getCustomerReview = await _unitOfWork.GetRepository<Coupon>().GetById(model.Id);
                if (getCustomerReview == null)
                    return new ApiResponse<CustomerReviewResponseViewModel>() { Success = false, Message = "Customer review Not Found !" };
                var updateReview = _mapper.Map<CustomerReview>(model);
                await _unitOfWork.GetRepository<CustomerReview>().Upsert(updateReview);
                await _unitOfWork.SaveChangesAsync();
                var data = await _unitOfWork.GetRepository<CustomerReview>().GetByPrimaryKeyWithRelatedEntitiesAsync(updateReview.Id);
                var response = _mapper.Map<CustomerReviewResponseViewModel>(updateReview);
                response.MerchantName = data.Merchant.MerchantName;
                return new ApiResponse<CustomerReviewResponseViewModel>() { Success = true, Message = "customer review updated successfully", Data = response };
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }
    }
}
