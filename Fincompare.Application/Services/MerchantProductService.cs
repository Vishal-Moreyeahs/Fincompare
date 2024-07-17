using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Models;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantProductResponse;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Services
{
    public class MerchantProductService : IMerchantProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MerchantProductService(IUnitOfWork unitOfWork, IMapper mapper)
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IEnumerable<MerchantProductViewModel>>> GetMerchantProductByMerchantId(int merchantId)
        {
            try
            {
                var response = new ApiResponse<IEnumerable<MerchantProductViewModel>>();

                //Get Merchant
                var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity()
                                            .Where(x => x.MerchantId == merchantId)
                                                .Select(x => new MerchantProductViewModel
                                                {
                                                    MerchantProductId = x.Id,
                                                    MerchantId = merchantId,
                                                    ServiceCategoryId = x.ServiceCategoryId,
                                                    ServiceCategoryName = x.ServiceCategory.ServCategoryName,
                                                    InstrumentId = x.InstrumentId,
                                                    InstrumentName = x.Instrument.InstrumentName,
                                                    ProductId = x.ProductId,
                                                    ProductName = x.Instrument.InstrumentName,
                                                    MerchantName = x.Merchant.MerchantName,
                                                    ReceiveCountry3Iso = x.ReceiveCountry3Iso,
                                                    SendCountry3Iso = x.SendCountry3Iso,
                                                    ReceiveCurrencyId = x.ReceiveCurrencyId,
                                                    SendCurrencyId = x.SendCurrencyId,
                                                    ServiceLevels = x.ServiceLevels,
                                                    Status = x.Status
                                                }).ToList();

                if (merchantProductList == null || merchantProductList.Count == 0)
                {
                    response.Message = "merchant products not found";
                    return response;
                }
                response.Status = true;
                response.Message = "Merchant product fetched";
                response.Data = merchantProductList;
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Process failed to fetch merchant products");
            }
            
        }

        public ApiResponse<IEnumerable<MerchantProductViewModel>> GetMerchantProducts(string sendCountry, string receiveCountry, string sendCurrency, string receiveCurrency, int? merchantID, int? merchantProductID, int? productID, int? serviceCategoryID, int? instrumentID, bool? status)
        {
            var response = new ApiResponse<IEnumerable<MerchantProductViewModel>>();
            var merchantProducts = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();


            // Apply filters
            if (merchantID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.MerchantId == merchantID.Value);
            if (merchantProductID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.Id == merchantProductID.Value);
            if (productID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.ProductId == productID.Value);
            if (serviceCategoryID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.ServiceCategoryId == serviceCategoryID.Value);
            if (instrumentID.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.InstrumentId == instrumentID.Value);
            if (status.HasValue)
                merchantProducts = merchantProducts.Where(mp => mp.Status == status.Value);

            // Filter by required path parameters
            merchantProducts = merchantProducts
                .Where(mp => mp.SendCountry3Iso == sendCountry)
                .Where(mp => mp.ReceiveCountry3Iso == receiveCountry)
                .Where(mp => mp.SendCurrencyId == sendCurrency)
                .Where(mp => mp.ReceiveCurrencyId == receiveCurrency);

            if (!merchantProducts.Any())
            {
                response.Message = "merchant products not found";
                return response;
            }
            var data = merchantProducts.Select(x => new MerchantProductViewModel
                                         {
                                             MerchantProductId = x.Id,
                                             MerchantId = x.MerchantId,
                                             ServiceCategoryId = x.ServiceCategoryId,
                                             ServiceCategoryName = x.ServiceCategory.ServCategoryName,
                                             InstrumentId = x.InstrumentId,
                                             InstrumentName = x.Instrument.InstrumentName,
                                             ProductId = x.ProductId,
                                             ProductName = x.Instrument.InstrumentName,
                                             MerchantName = x.Merchant.MerchantName,
                                             ReceiveCountry3Iso = x.ReceiveCountry3Iso,
                                             SendCountry3Iso = x.SendCountry3Iso,
                                             ReceiveCurrencyId = x.ReceiveCurrencyId,
                                             SendCurrencyId = x.SendCurrencyId,
                                             ServiceLevels = x.ServiceLevels,
                                             Status = x.Status
                                         }).ToList();

            response.Status = true;
            response.Message = "Merchant Products found";
            response.Data = data;
            return response;
        }
    }
}
