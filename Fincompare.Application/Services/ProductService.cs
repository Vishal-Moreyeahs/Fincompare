using AutoMapper;
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

        public async Task<ApiResponse<string>> CreateProduct(CreateProductRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<string>() { Status = false, Message = "Request Not Accepted" };
                var addRequest = _mapper.Map<Product>(model);
                await _unitOfWork.GetRepository<Product>().Add(addRequest);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Status = true, Message = "Product Creation Successfully!" };

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ApiResponse<string>> UpdateProduct(UpdateProductRequest model)
        {
            try
            {
                var checkProduct = await _unitOfWork.GetRepository<Product>().GetById(model.Id);
                if (checkProduct == null)
                    return new ApiResponse<string>() { Status = false, Message = "Product Not Found !" };
                var updateRequest = _mapper.Map(model, checkProduct);
                await _unitOfWork.GetRepository<Product>().Upsert(updateRequest);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Status = true, Message = "Product Update Successfully!" };

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<GetAllProductResponse>>> GetAllProduct()
        {
            var getData = await _unitOfWork.GetRepository<Product>().GetAll();
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
