using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantRemitProductRateRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantRemitProductRateResponse;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Response.MerchantRemitFeeResponse.MerchantRemitFeeBaseResponse;

namespace Fincompare.Application.Services
{
    public class MerchantRemitProductRateService : IMerchantRemitProductRateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MerchantRemitProductRateService(IUnitOfWork unitOfWork, IMapper mapper)
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<MerchantRemitProductRateViewModel>> AddMerchantRemitProductRate(AddMerchantRemitProductRateRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantRemitProductRateViewModel>() { Status = false, Message = "Request are UnVailed !" };

                var requestData = _mapper.Map<MerchantRemitProductRate>(model);

                await _unitOfWork.GetRepository<MerchantRemitProductRate>().Add(requestData);
                await _unitOfWork.SaveChangesAsync();

                var merchantRemitRate = await _unitOfWork.GetRepository<MerchantRemitProductRate>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(requestData.Id);

                var data = new MerchantRemitProductRateViewModel
                {
                    Id = merchantRemitRate.Id, // Replace with appropriate values
                    MerchantRateRef = merchantRemitRate.MerchantRateRef, // Replace with appropriate values
                    MerchantId = 1001, // Replace with appropriate values
                    MerchantName = "Example Merchant", // Replace with appropriate values
                    MerchantProductId = 123, // Replace with appropriate values
                    ProductName = "Example Product", // Replace with appropriate values
                    InstrumentName = "Instrument A", // Replace with appropriate values
                    ServiceCategoryName = "Category X", // Replace with appropriate values
                    SendCountry3Iso = "USA", // Replace with appropriate values
                    ReceiveCountry3Iso = "GBR", // Replace with appropriate values
                    SendCur = "USD", // Replace with appropriate values
                    ReceiveCur = "GBP", // Replace with appropriate values
                    SendMinLimit = 100, // Replace with appropriate values
                    SendMaxLimit = 10000, // Replace with appropriate values
                    ReceiveMinLimit = 90, // Replace with appropriate values
                    ReceiveMaxLimit = 9000, // Replace with appropriate values
                    Rate = 1.25, // Replace with appropriate values
                    PromoRate = 1.20, // Replace with appropriate values
                    ValidityExpiry = DateTime.UtcNow.AddDays(30), // Replace with appropriate values
                    Status = true // Replace with appropriate values
                };

                return new ApiResponse<MerchantRemitProductRateViewModel>() { Status = true, Message = "Merchant Remittance Product Rate created successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}
