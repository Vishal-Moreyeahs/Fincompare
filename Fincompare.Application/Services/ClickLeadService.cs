using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.ClickLeadRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.ClickLeadResponse;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Response.CouponResponse.CouponResponseViewModel;
using static Fincompare.Application.Response.GroupMerchantResponse.GroupMerchantViewResponse;

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
                    return new ApiResponse<ClickLeadResponseViewModel>() { Status = false, Message = "click redirection creation failed" };

                var addClick = _mapper.Map<ClickLead>(model);
                await _unitOfWork.GetRepository<ClickLead>().Add(addClick);
                await _unitOfWork.SaveChangesAsync();
                var data = await _unitOfWork.GetRepository<ClickLead>().GetByPrimaryKeyWithRelatedEntitiesAsync(addClick.Id);
               
                var response = _mapper.Map<ClickLeadResponseViewModel>(data);
                response.MerchantName = data.Merchant.MerchantName;

                return new ApiResponse<ClickLeadResponseViewModel>() { Status = true, Message = "Click lead created successfully", Data = response };

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<IEnumerable<ClickLeadResponseViewModel>>> GetAllClickLeadRecords(int? merchantId, int? clickLeadId, int? customerId, string country3iso)
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
                if (getData.Count == 0)
                    return new ApiResponse<IEnumerable<ClickLeadResponseViewModel>>() { Status = false, Message = "Click Lead records not found!" };
                return new ApiResponse<IEnumerable<ClickLeadResponseViewModel>>() { Status = true, Message = "Click lead records Found!", Data = getData };
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }
    }
}
