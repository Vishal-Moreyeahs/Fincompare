using AutoMapper;
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
                    return new ApiResponse<CreateServiceCategoriesRequest>() { Status = false, Message = "Request Invalid", Data = model };
                var addService = _mapper.Map<ServiceCategory>(model);
                await _unitOfWork.GetRepository<ServiceCategory>().Add(addService);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<CreateServiceCategoriesRequest>() { Status = true, Message = "Service Category created successfully", Data = model };
            }
            catch (Exception)
            {

                throw;
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
                    return new ApiResponse<IEnumerable<GetAllServiceCategoriesResponse>>() { Status = true, Message = "Service Categories Fetch Successfully", Data = response };
                return new ApiResponse<IEnumerable<GetAllServiceCategoriesResponse>>() { Status = true, Message = "Service Categories Not Found" };

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResponse<CreateServiceCategoriesRequest>> UpdateServiceCategories(UpdateServiceCategoriesRequest model)
        {
            try
            {
                var getServiceCategories = await _unitOfWork.GetRepository<ServiceCategory>().GetById(model.Id);
                if (getServiceCategories == null)
                    return new ApiResponse<CreateServiceCategoriesRequest>() { Status = false, Message = "Service Categories Not Found" };
                var updateResponse = _mapper.Map<ServiceCategory>(model);
                await _unitOfWork.GetRepository<ServiceCategory>().Upsert(updateResponse);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<CreateServiceCategoriesRequest>(updateResponse);
                return new ApiResponse<CreateServiceCategoriesRequest>() { Status = true, Message = "Service Categories Updated Successfully", Data = response };

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
