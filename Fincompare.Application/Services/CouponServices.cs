﻿using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.CouponRequest.CouponRequestViewModel;
using static Fincompare.Application.Response.CouponResponse.CouponResponseViewModel;

namespace Fincompare.Application.Services
{
    public class CouponServices : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CouponServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<FetchCouponResponse>> CreateCoupon(CreateCouponRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<FetchCouponResponse>() { Success = false, Message = "coupon creation failed" };

                var addCoupon = _mapper.Map<Coupon>(model);
                await _unitOfWork.GetRepository<Coupon>().Add(addCoupon);
                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<FetchCouponResponse>(addCoupon);

                return new ApiResponse<FetchCouponResponse>() { Success = true, Message = "coupon record created successfully", Data = response };

            }
            catch (Exception ex)
            {

                throw new ApplicationException($"coupon creation failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<FetchCouponResponse>> UpdateCoupon(UpdateCouponRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<FetchCouponResponse>() { Success = false, Message = "coupon update failed" };
                var getCoupon = await _unitOfWork.GetRepository<Coupon>().GetById(model.Id);
                if (getCoupon == null)
                    return new ApiResponse<FetchCouponResponse>() { Success = false, Message = "coupon not found" };
                var updateCoupon = _mapper.Map<Coupon>(model);
                await _unitOfWork.GetRepository<Coupon>().Upsert(updateCoupon);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<FetchCouponResponse>(updateCoupon);
                return new ApiResponse<FetchCouponResponse>() { Success = true, Message = "coupon record updated successfully", Data = response };
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"coupon update failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<FetchCouponResponse>>> GetAllCoupon(int? couponId, bool? status)
        {
            try
            {
                var getAllCoupon = await _unitOfWork.GetRepository<Coupon>().GetAll();
                if (couponId.HasValue)
                {
                    getAllCoupon = getAllCoupon.Where(x => x.Id == couponId.Value).ToList();
                }
                if (status.HasValue)
                {
                    getAllCoupon = getAllCoupon.Where(x => x.Status == status.Value).ToList();
                }
                var response = _mapper.Map<IEnumerable<FetchCouponResponse>>(getAllCoupon);
                if (getAllCoupon.ToList().Count > 0)
                    return new ApiResponse<IEnumerable<FetchCouponResponse>>() { Success = true, Message = "coupan record fetched successfully", Data = response };
                return new ApiResponse<IEnumerable<FetchCouponResponse>>() { Success = false, Message = "coupon fetch failed" };

            }
            catch (Exception ex)
            {

                throw new ApplicationException($"coupon fetch failed {ex.Message}");
            }
        }

    }
}
