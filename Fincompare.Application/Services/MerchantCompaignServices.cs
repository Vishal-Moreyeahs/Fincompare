using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantCompaignRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantCompaignResponse;
using Fincompare.Application.Response.MerchantProductResponse;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Response.MerchantRemitFeeResponse.MerchantRemitFeeBaseResponse;

namespace Fincompare.Application.Services
{
    public class MerchantCompaignServices : IMerchantCompaignServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MerchantCompaignServices(IUnitOfWork unitOfWork, IMapper mapper)
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<MerchantCompaignResponseViewModel>> AddMerchantCompaign(AddMerchantCompaignRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantCompaignResponseViewModel>()
                    {
                        Success = false,
                        Message = "Merchant Compaign Creation Failed"
                    };
                //var checkMerchantExist = await _unitOfWork.GetRepository<Merchant>().GetById(model.MerchantId);
                var createdData = _mapper.Map<MerchantCampaign>(model);
                await _unitOfWork.GetRepository<MerchantCampaign>().Add(createdData);
                await _unitOfWork.SaveChangesAsync();
                var responseData = await _unitOfWork.GetRepository<MerchantCampaign>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(createdData.Id);
                var merchantProduct = new MerchantProduct();
                if (responseData.MerchantProductId.HasValue)
                { 
                    merchantProduct = await _unitOfWork.GetRepository<MerchantProduct>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(responseData.MerchantProductId.Value);
                }

                var merchantResponseData = new MerchantCompaignResponseViewModel
                {
                    MerchantCampaignId = responseData.Id,
                    CampaignCode = responseData.CampaignCode,
                    CampaignDescription = responseData.CampaignDescription,
                    MerchantID = responseData.MerchantId,
                    MerchantName = responseData.Merchant.MerchantName,
                    ServiceCategoryId = responseData.ServiceCategoryId,
                    ServiceCategoryName = responseData.ServiceCategory.ServCategoryName,
                    InstrumentId = merchantProduct !=null ? merchantProduct.InstrumentId : 0,
                    InstrumentName = merchantProduct != null ? merchantProduct.Instrument.InstrumentName: null,
                    ProductId = merchantProduct != null ? merchantProduct.ProductId: 0,
                    ProductName = merchantProduct != null ? merchantProduct.Product.ProductName : null,
                    MerchantProductId = responseData.MerchantProductId.HasValue ? responseData.MerchantProductId.Value : null,
                    ReceiveCountry3Iso = responseData.ReceiveCountry3Iso,
                    SendCountry3Iso = responseData.SendCountry3Iso,
                    Status = responseData.Status,
                    DateActive = responseData.DateActive,
                    DateValidity = responseData.DateValidity
                };


                var response = new ApiResponse<MerchantCompaignResponseViewModel>()
                {
                    Success = true,
                    Message = "Merchant compaign created successfully",
                    Data = merchantResponseData

                };
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error in adding merchant compaign insertion");
            }
        }

        public async Task<ApiResponse<IEnumerable<MerchantCompaignResponseViewModel>>> GetMerchantCampaigns(int? MerchantCampaignId, int? MerchantID, string sendCountry, string receiveCountry, /*string sendCurrency, string receiveCurrency,*/ int? MerchantProductID, int? serviceCategoryId, int? instrumentId, DateTime? dateValidity/*, decimal? SendMinLimit, decimal? ReceiveMinLimit*/)
        {
            var query = _unitOfWork.GetRepository<MerchantCampaign>().GetAllRelatedEntity().AsQueryable();
            var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();

            if (MerchantCampaignId.HasValue)
                query = query.Where(mc => mc.Id == MerchantCampaignId.Value);

            if (MerchantID.HasValue)
                query = query.Where(mc => mc.MerchantId == MerchantID.Value);

            if (!string.IsNullOrEmpty(sendCountry))
                query = query.Where(mc => mc.SendCountry3Iso == sendCountry);

            if (!string.IsNullOrEmpty(receiveCountry))
                query = query.Where(mc => mc.ReceiveCountry3Iso == receiveCountry);
            if (dateValidity.HasValue)
                query = query.Where(mc => mc.DateValidity <= dateValidity);

            //if (!string.IsNullOrEmpty(sendCurrency))
            //    query = query.Where(mc => mc == sendCurrency);

            //if (!string.IsNullOrEmpty(receiveCurrency))
            //    query = query.Where(mc => mc.ReceiveCurrency == receiveCurrency);

            if (MerchantProductID.HasValue)
                query = query.Where(mc => mc.MerchantProductId == MerchantProductID.Value);

            if (serviceCategoryId.HasValue)
                query = query.Where(mc => mc.ServiceCategoryId == serviceCategoryId.Value);

            if (instrumentId.HasValue)
                query = query.Where(mc => mc.MerchantProduct.InstrumentId == instrumentId.Value);

            //if (SendMinLimit.HasValue)
            //    query = query.Where(mc => mc.SendMinLimit >= SendMinLimit.Value);

            //if (ReceiveMinLimit.HasValue)
            //    query = query.Where(mc => mc.ReceiveMinLimit >= ReceiveMinLimit.Value);

            var data = (from merchantCompaign in query
                        join mp in merchantProductList
                        on merchantCompaign.MerchantProductId equals mp.Id
                        select new MerchantCompaignResponseViewModel
                        {
                            MerchantCampaignId = merchantCompaign.Id,
                            MerchantID = merchantCompaign.MerchantId,
                            MerchantName = merchantCompaign.Merchant.MerchantName,
                            ServiceCategoryId = merchantCompaign.MerchantProduct.ServiceCategoryId,
                            ServiceCategoryName = mp.ServiceCategory.ServCategoryName,
                            InstrumentId = mp.Instrument.Id,
                            InstrumentName = mp.Instrument.InstrumentName,
                            ProductId = merchantCompaign.MerchantProduct.ProductId,
                            ProductName = mp.Product.ProductName,
                            MerchantProductId = merchantCompaign.MerchantProductId,
                            SendCountry3Iso = merchantCompaign.SendCountry3Iso,
                            ReceiveCountry3Iso = merchantCompaign.ReceiveCountry3Iso,
                            DateActive = merchantCompaign.DateActive,
                            DateValidity = merchantCompaign.DateValidity,
                            CampaignDescription = merchantCompaign.CampaignDescription,
                            CampaignCode = merchantCompaign.CampaignCode,
                            Status = merchantCompaign.Status
                        }).ToList();


            if (data.Count > 0)
                return new ApiResponse<IEnumerable<MerchantCompaignResponseViewModel>>() { Success = true, Message = "Merchant campaign record fetched successfully", Data = data };
            return new ApiResponse<IEnumerable<MerchantCompaignResponseViewModel>>() { Success = false, Message = "Merchant campaign fetch failed" };
        }

        public async Task<ApiResponse<MerchantCompaignResponseViewModel>> UpdateMerchantCompaign(UpdateMerchantCompaignRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantCompaignResponseViewModel>()
                    {
                        Success = false,
                        Message = "Merchant Compaign Updation Failed"
                    };
                var checkData = await _unitOfWork.GetRepository<MerchantCampaign>().GetById(model.Id);
                if (checkData == null)
                    return new ApiResponse<MerchantCompaignResponseViewModel>() { Success = false, Message = "Merchant compaign Not Found!" };

                var requestData = _mapper.Map(model, checkData);
                await _unitOfWork.GetRepository<MerchantCampaign>().Upsert(requestData);
                await _unitOfWork.SaveChangesAsync();
                var responseData = await _unitOfWork.GetRepository<MerchantCampaign>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(model.Id);
                var merchantProduct = new MerchantProduct();
                if (responseData.MerchantProductId.HasValue)
                {
                    merchantProduct = await _unitOfWork.GetRepository<MerchantProduct>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(responseData.MerchantProductId.Value);
                }

                var merchantResponseData = new MerchantCompaignResponseViewModel
                {
                    MerchantCampaignId = responseData.Id,
                    CampaignCode = responseData.CampaignCode,
                    CampaignDescription = responseData.CampaignDescription,
                    MerchantID = responseData.MerchantId,
                    MerchantName = responseData.Merchant.MerchantName,
                    ServiceCategoryId = responseData.ServiceCategoryId,
                    ServiceCategoryName = responseData.ServiceCategory.ServCategoryName,
                    InstrumentId = merchantProduct != null ? merchantProduct.InstrumentId : 0,
                    InstrumentName = merchantProduct != null ? merchantProduct.Instrument.InstrumentName : null,
                    ProductId = merchantProduct != null ? merchantProduct.ProductId : 0,
                    ProductName = merchantProduct != null ? merchantProduct.Product.ProductName : null,
                    MerchantProductId = responseData.MerchantProductId.HasValue ? responseData.MerchantProductId.Value : null,
                    ReceiveCountry3Iso = responseData.ReceiveCountry3Iso,
                    SendCountry3Iso = responseData.SendCountry3Iso,
                    Status = responseData.Status,
                    DateActive = responseData.DateActive,
                    DateValidity = responseData.DateValidity
                };


                var response = new ApiResponse<MerchantCompaignResponseViewModel>()
                {
                    Success = true,
                    Message = "Merchant compaign updated successfully",
                    Data = merchantResponseData

                };
                return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error in updating merchant compaign insertion");
            }
        }
    }
}
