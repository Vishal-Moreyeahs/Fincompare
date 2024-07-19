﻿using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.ProductRequests.ProductRequestViewModel;
using static Fincompare.Application.Response.ProductResponse.ProductResponseBaseClass;

namespace Fincompare.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetAllProductResponse>> CreateProduct(CreateProductRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<GetAllProductResponse>() { Status = false, Message = "Request Not Accepted" };
                var addRequest = _mapper.Map<Product>(model);
                await _unitOfWork.GetRepository<Product>().Add(addRequest);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<GetAllProductResponse>(addRequest);
                return new ApiResponse<GetAllProductResponse>() { Status = true, Message = "Product created successfully!", Data = response };

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ApiResponse<CreateProductRequest>> UpdateProduct(UpdateProductRequest model)
        {
            try
            {
                var checkProduct = await _unitOfWork.GetRepository<Product>().GetById(model.Id);
                if (checkProduct == null)
                    return new ApiResponse<CreateProductRequest>() { Status = false, Message = "Product Not Found !" };
                var updateRequest = _mapper.Map(model, checkProduct);
                await _unitOfWork.GetRepository<Product>().Upsert(updateRequest);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<CreateProductRequest>(updateRequest);
                return new ApiResponse<CreateProductRequest>() { Status = true, Message = "product updated successfully!", Data = response };

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<GetAllProductResponse>>> GetAllProduct(string? countryIso3, int? idProduct, int? idServCategory, bool? status)
        {
            var getData = await _unitOfWork.GetRepository<Product>().GetAll();
            if (!string.IsNullOrEmpty(countryIso3))
            {
                getData = getData.Where(x => x.Country3Iso == countryIso3).ToList();
            }
            if (idProduct.HasValue)
            {
                getData = getData.Where(x => x.Id == idProduct.Value).ToList();
            }
            if (idServCategory.HasValue)
            {
                getData = getData.Where(x => x.ServiceCategoryId == idServCategory.Value).ToList();
            }
            if (status.HasValue)
            {
                getData = getData.Where(x => x.Status == status).ToList();
            }
            var getProductList = _mapper.Map<IEnumerable<GetAllProductResponse>>(getData).ToList();
            if (getProductList.Count == 0)
                return new ApiResponse<IEnumerable<GetAllProductResponse>>() { Status = false, Message = "Product Not Found !" };
            return new ApiResponse<IEnumerable<GetAllProductResponse>>() { Status = false, Message = "Product List Found !", Data = getProductList };

        }
        public async Task<ApiResponse<GetAllProductResponse>> GetProductById(int id)
        {
            var getData = await _unitOfWork.GetRepository<Product>().GetById(id);
            var getProductList = _mapper.Map<GetAllProductResponse>(getData);
            if (getProductList == null)
                return new ApiResponse<GetAllProductResponse>() { Status = false, Message = "Product Not Found !" };
            return new ApiResponse<GetAllProductResponse>() { Status = true, Message = "Product Found !", Data = getProductList };

        }
    }
}
