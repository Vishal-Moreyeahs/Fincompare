﻿using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.ServiceCategoriesRequest.ServiceCategoriesViewModel;
using static Fincompare.Application.Response.ServiceCategoriesResponse.ServiceCategoriesViewResponse;

namespace Fincompare.Application.Services
{
    public class ServiceCategories : IServiceCategory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ServiceCategories(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ApiResponse<CreateServiceCategoriesRequest>> CreateServiceCategories(CreateServiceCategoriesRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<CreateServiceCategoriesRequest>() { Success = false, Message = "service category creation failed", Data = model };

                var getAllServiceCategories = await _unitOfWork.GetRepository<ServiceCategory>().GetAll();

                if (getAllServiceCategories.Any(x => x.ServCategoryName == model.ServiceCategoryName && x.Country3Iso == model.Country3Iso))
                {
                    return new ApiResponse<CreateServiceCategoriesRequest>() { Success = false, Message = "Service category with the same name and country already exists", Data = model };
                }

                var addService = _mapper.Map<ServiceCategory>(model);
                await _unitOfWork.GetRepository<ServiceCategory>().Add(addService);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<CreateServiceCategoriesRequest>() { Success = true, Message = "Service Category record created successfully", Data = model };
            }
            catch (Exception)
            {

                throw new ApplicationException("service category creation failed");
            }
        }

        public async Task<ApiResponse<IEnumerable<GetAllServiceCategoriesResponse>>> FetchAllServiceCategories(int? idServCategory, string? countryIso3, bool? status)
        {
            try
            {
                var getAllServiceCategories = await _unitOfWork.GetRepository<ServiceCategory>().GetAll();
                if (idServCategory.HasValue)
                {
                    getAllServiceCategories = getAllServiceCategories.Where(x => x.Id == idServCategory.Value).ToList();
                }
                if (!string.IsNullOrEmpty(countryIso3))
                {
                    getAllServiceCategories = getAllServiceCategories.Where(x => x.Country3Iso == countryIso3).ToList();
                }
                if (status.HasValue)
                {
                    getAllServiceCategories = getAllServiceCategories.Where(x => x.Status == status.Value).ToList();
                }

                var response = _mapper.Map<IEnumerable<GetAllServiceCategoriesResponse>>(getAllServiceCategories);


                if (getAllServiceCategories.ToList().Count > 0)
                    return new ApiResponse<IEnumerable<GetAllServiceCategoriesResponse>>() { Success = true, Message = "Service Categories record fetched successfully", Data = response };
                return new ApiResponse<IEnumerable<GetAllServiceCategoriesResponse>>() { Success = false, Message = "Service Category fetch failed" };

            }
            catch (Exception ex)
            {
                throw new ApplicationException("service category fetch failed");
            }
        }

        public async Task<ApiResponse<CreateServiceCategoriesRequest>> UpdateServiceCategories(UpdateServiceCategoriesRequest model)
        {
            try
            {
                var getServiceCategories = await _unitOfWork.GetRepository<ServiceCategory>().GetById(model.Id);
                if (getServiceCategories == null)
                    return new ApiResponse<CreateServiceCategoriesRequest>() { Success = false, Message = "Service Category update failed" };
                var updateResponse = _mapper.Map<ServiceCategory>(model);
                await _unitOfWork.GetRepository<ServiceCategory>().Upsert(updateResponse);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<CreateServiceCategoriesRequest>(updateResponse);
                return new ApiResponse<CreateServiceCategoriesRequest>() { Success = true, Message = "Service Category record updated successfully", Data = response };

            }
            catch (Exception)
            {

                throw new ApplicationException("service category update failed");
            }
        }

    }
}
