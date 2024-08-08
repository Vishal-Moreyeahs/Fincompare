using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.ClickLeadRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.ClickLeadResponse;
using Fincompare.Domain.Entities;

namespace Fincompare.Application.Services
{
    public class ClickLeadService : IClickLeadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClickLeadService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<ClickLeadResponseViewModel>> AddClickLeadRedirections(AddClickLeadRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<ClickLeadResponseViewModel>() { Success = false, Message = "click lead creation failed" };

                var addClick = _mapper.Map<ClickLead>(model);
                await _unitOfWork.GetRepository<ClickLead>().Add(addClick);
                await _unitOfWork.SaveChangesAsync();
                var data = await _unitOfWork.GetRepository<ClickLead>().GetByPrimaryKeyWithRelatedEntitiesAsync(addClick.Id);

                var response = _mapper.Map<ClickLeadResponseViewModel>(data);
                response.MerchantName = data.Merchant.MerchantName;

                return new ApiResponse<ClickLeadResponseViewModel>() { Success = true, Message = "Click lead record created successfully", Data = response };

            }
            catch (Exception ex)
            {
                throw new ArgumentException($"click lead creation failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<ClickLeadResponseViewModel>>> GetAllClickLeadRecords(int? merchantId, int? clickLeadId, int? customerId, string? country3iso)
        {
            try
            {
                var getAllClickLead = _unitOfWork.GetRepository<ClickLead>().GetAllRelatedEntity();


                if (merchantId.HasValue)
                    getAllClickLead = getAllClickLead.Where(mp => mp.MerchantId == merchantId.Value);
                if (!string.IsNullOrEmpty(country3iso))
                    getAllClickLead = getAllClickLead.Where(mp => mp.Country3Iso == country3iso);
                if (clickLeadId.HasValue)
                    getAllClickLead = getAllClickLead.Where(mp => mp.Id == clickLeadId.Value);
                if (customerId.HasValue)
                    getAllClickLead = getAllClickLead.Where(mp => mp.CustomerUserId == customerId.Value); 


                var getData = getAllClickLead
                    .Select(x => new ClickLeadResponseViewModel
                    {
                        Date = x.Date,
                        CustomerUserId = x.CustomerUserId,
                        MerchantId = x.MerchantId,
                        MerchantName = x.Merchant.MerchantName,
                        Country3Iso = x.Country3Iso,
                        RoutingParamters = x.RoutingParamters,
                    }).ToList();
                if (getData.Count == 0 || getData == null)
                    return new ApiResponse<IEnumerable<ClickLeadResponseViewModel>>() { Success = false, Message = "click lead fetch failed" };
                return new ApiResponse<IEnumerable<ClickLeadResponseViewModel>>() { Success = true, Message = "click lead record fetched successfully", Data = getData };
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"click lead fetch failed {ex.Message}");
            }
        }
    }
}
