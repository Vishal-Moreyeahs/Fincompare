﻿using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantRemitProductRateResponse;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.MerchantRemitProductFeeRequests.MerchantRemitProductFeeRequestViewModel;
using static Fincompare.Application.Response.MerchantRemitFeeResponse.MerchantRemitFeeBaseResponse;

namespace Fincompare.Application.Services
{
    public class MerchantRemitFee : IMerchantRemitFee
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMerchantProductService _merchantProductService;
        private readonly IMapper _mapper;
        private readonly IMerchantPermissionService _permissionService;
        public MerchantRemitFee(IUnitOfWork unitOfWork, IMapper mapper, IMerchantProductService merchantProductService, IMerchantPermissionService permissionService)
        {
            _unitOfWork = unitOfWork;
            _merchantProductService = merchantProductService;
            _mapper = mapper;
            _permissionService = permissionService;
        }
        public async Task<ApiResponse<MerchantRemittanceFee>> AddMerchantRemitFee(CreateMerchantRemitProductFeeRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantRemittanceFee>() { Success = false, Message = "merchant product remit fee creation failed" };

                var isAuthenticatedMerchant = await _permissionService.CheckMerchantPermission(model.MerchantId);
                if (!isAuthenticatedMerchant)
                {
                    return new ApiResponse<MerchantRemittanceFee>() { Success = false, Message = "Invalid/Unauthorized merchant" };

                }
                var instrumentCheck = await _unitOfWork.GetRepository<Instrument>().GetById(model.PayInInstrumentId.Value);

                if (instrumentCheck.InstrumentType.Trim().ToLower() != "payin")
                {
                    return new ApiResponse<MerchantRemittanceFee>() { Success = false, Message = "PayinInstrumentId not matches with payin instrument type" };

                }

                var merchantProductResponse = await _merchantProductService.GetMerchantProducts(model.SendCountry3Iso,model.ReceiveCountry3Iso,model.SendCurrency,model.ReceiveCurrency,model.MerchantId,null,model.ProductId,model.ServiceCategoryId,model.PayoutInstrumentId, true);


                if (!merchantProductResponse.Success && merchantProductResponse.Data == null)
                {
                    return new ApiResponse<MerchantRemittanceFee>() { Success = false, Message = "merchant product not found for specified service category, instrument and product." };
                }
                var merchantProductIdCheck = merchantProductResponse.Data.ToList().FirstOrDefault();

                if (merchantProductIdCheck == null)
                {
                    return new ApiResponse<MerchantRemittanceFee>() { Success = false, Message = "merchant product not found for specified service category, instrument and product." };
                }

               // if
               //(merchantProductIdCheck.SendCountry3Iso.Trim().ToUpper() != model.SendCountry3Iso.Trim().ToUpper())
               // {

               //     return new ApiResponse<MerchantRemittanceFee>()
               //     { Success = false, Message = "The specified 'SendCountry3Iso' does not match the merchant product's value." };
               // }
               // if
               // (merchantProductIdCheck.ReceiveCountry3Iso.Trim().ToUpper() != model.ReceiveCountry3Iso.Trim().ToUpper())
               // {

               //     return new ApiResponse<MerchantRemittanceFee>()
               //     { Success = false, Message = "The specified 'ReceiveCountry3Iso' does not match the merchant product's value." };
               // }
               // if
               // (merchantProductIdCheck.SendCurrencyId.Trim().ToUpper() != model.SendCurrency.Trim().ToUpper())
               // {
               //     return new ApiResponse<MerchantRemittanceFee>()
               //     { Success = false, Message = "The specified 'SendCurrency' does not match the merchant product's value." };
               // }
               // if
               // (merchantProductIdCheck.ReceiveCurrencyId.Trim().ToUpper() != model.ReceiveCurrency.Trim().ToUpper())
               // {
               //     return new ApiResponse<MerchantRemittanceFee>()
               //     { Success = false, Message = "The specified 'ReceiveCurrency' does not match the merchant product's value." }; ;
               // }

                var requestData = _mapper.Map<MerchantRemitProductFee>(model);
                requestData.MerchantProductId = merchantProductIdCheck.Id;
                await _unitOfWork.GetRepository<MerchantRemitProductFee>().Add(requestData);
                await _unitOfWork.SaveChangesAsync();

                var merchantRemitFee = await _unitOfWork.GetRepository<MerchantRemitProductFee>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(requestData.Id);
                var merchantProduct = await _unitOfWork.GetRepository<MerchantProduct>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>((int)merchantRemitFee.MerchantProductId);
                var data = new MerchantRemittanceFee
                {
                    Id = merchantRemitFee.Id,
                    MerchantId = merchantRemitFee.MerchantId,
                    MerchantName = merchantRemitFee.Merchant.MerchantName,
                    ServiceCategoryId = merchantRemitFee.MerchantProduct.ServiceCategoryId,
                    ServiceCategoryName = merchantProduct.ServiceCategory.ServCategoryName,
                    PayoutInstrumentId = merchantProduct.Instrument.Id,
                    PayoutInstrumentName = merchantProduct.Instrument.InstrumentName,
                    ProductId = merchantProduct.ProductId,
                    ProductName = merchantProduct.Product.ProductName,
                    FeesName = merchantRemitFee.FeesName,
                    FeesCurrency = merchantRemitFee.FeesCur,
                    Fees = merchantRemitFee.Fees,
                    PromoFees = merchantRemitFee.PromoFees,
                    MerchantProductId = merchantRemitFee.MerchantProductId,
                    SendCountry3Iso = merchantRemitFee.SendCountry3Iso,
                    ReceiveCountry3Iso = merchantRemitFee.ReceiveCurrency,
                    SendCurrency = merchantRemitFee.SendCurrency,
                    ReceiveCurrency = merchantRemitFee.ReceiveCurrency,
                    SendMinLimit = merchantRemitFee.SendMinLimit,
                    SendMaxLimit = merchantRemitFee.SendMaxLimit,
                    ReceiveMinLimit = merchantRemitFee.ReceiveMinLimit,
                    ReceiveMaxLimit = merchantRemitFee.ReceiveMaxLimit,
                    ValidityExpiry = merchantRemitFee.ValidityExpiry,
                    PayInInstrumentId = merchantRemitFee.PayInInstrumentId,
                    PayInInstrumentName = merchantRemitFee.Instruments.InstrumentName,
                    VariableFee = merchantRemitFee.VariableFee,
                    Status = merchantRemitFee.Status
                };

                return new ApiResponse<MerchantRemittanceFee>() { Success = true, Message = "Merchant Product Remittance Fee record created successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException($"merchant product remit fee creation failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>>
            GetMerchantRemittanceFee
            (string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            int? merchantId,
            int? remittanceFeeId,
            int? merchantProductId,
            int? serviceCategoryId,
            int? instrumentId,
            double? sendMinLimit,
            double? receiveMinLimit,
            bool? status,
            bool? isValid)


        {
            //var response = new ApiResponse<IEnumerable<MerchantRemittanceFee>>();
            var merchantRemitFees = _unitOfWork.GetRepository<MerchantRemitProductFee>().GetAllRelatedEntity().AsQueryable();
            var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();

            // Apply filters
            if (merchantId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantId == merchantId.Value);
            if (merchantProductId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantProductId == merchantProductId.Value);
            if (remittanceFeeId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.Id == remittanceFeeId.Value);
            if (serviceCategoryId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantProduct.ServiceCategoryId == serviceCategoryId.Value);
            if (instrumentId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantProduct.InstrumentId == instrumentId.Value);
            if (sendMinLimit.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.SendMinLimit >= sendMinLimit.Value);
            if (receiveMinLimit.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.ReceiveMinLimit <= receiveMinLimit.Value);
            if (status.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.Status == status.Value);
            //if (isValid.HasValue)
            //    merchantRemitFees = merchantRemitFees.Where(mp => mp.va == status.Value);

            // Filter by required path parameters
            merchantRemitFees = merchantRemitFees
                .Where(mp => mp.SendCountry3Iso == sendCountry)
                .Where(mp => mp.ReceiveCountry3Iso == receiveCountry)
                .Where(mp => mp.SendCurrency == sendCurrency)
                .Where(mp => mp.ReceiveCurrency == receiveCurrency);

            //if (!merchantRemitFees.Any())
            //{
            //    response.Message = "merchant products not found";
            //    return response;
            //}

            var data = (from merchantRemitFee in merchantRemitFees
                        join mp in merchantProductList
                        on merchantRemitFee.MerchantProductId equals mp.Id
                        select new MerchantRemittanceFee
                        {
                            Id = merchantRemitFee.Id,
                            MerchantId = merchantRemitFee.MerchantId,
                            MerchantName = merchantRemitFee.Merchant.MerchantName,
                            ServiceCategoryId = merchantRemitFee.MerchantProduct.ServiceCategoryId,
                            ServiceCategoryName = mp.ServiceCategory.ServCategoryName,
                            PayoutInstrumentId = mp.Instrument.Id,
                            PayoutInstrumentName = mp.Instrument.InstrumentName,
                            ProductId = merchantRemitFee.MerchantProduct.ProductId,
                            ProductName = mp.Product.ProductName,
                            FeesName = merchantRemitFee.FeesName,
                            FeesCurrency = merchantRemitFee.FeesCur,
                            Fees = merchantRemitFee.Fees,
                            PromoFees = merchantRemitFee.PromoFees,
                            MerchantProductId = merchantRemitFee.MerchantProductId,
                            SendCountry3Iso = merchantRemitFee.SendCountry3Iso,
                            ReceiveCountry3Iso = merchantRemitFee.ReceiveCountry3Iso,
                            SendCurrency = merchantRemitFee.SendCurrency,
                            ReceiveCurrency = merchantRemitFee.ReceiveCurrency,
                            SendMinLimit = merchantRemitFee.SendMinLimit,
                            SendMaxLimit = merchantRemitFee.SendMaxLimit,
                            ReceiveMinLimit = merchantRemitFee.ReceiveMinLimit,
                            ReceiveMaxLimit = merchantRemitFee.ReceiveMaxLimit,
                            ValidityExpiry = merchantRemitFee.ValidityExpiry,
                            PayInInstrumentId = merchantRemitFee.PayInInstrumentId,
                            PayInInstrumentName = merchantRemitFee.Instruments.InstrumentName,
                            VariableFee = merchantRemitFee.VariableFee,
                            Status = merchantRemitFee.Status
                        }).ToList();

            if (data.Count > 0)
                return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Success = true, Message = "merchant product remmitance fee record fetch successfully", Data = data };
            return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Success = false, Message = "merchant product remmitance fee fetch failed" };

        }


        public async Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>> GetMerchantRemittanceFeeByMerchant(int merchantId, string sendCountry, string receiveCountry, string sendCurrency, string receiveCurrency, int? remittanceFeeId, int? merchantProductId, int? serviceCategoryId, int? instrumentId, double? sendMinLimit, double? receiveMinLimit, bool? status, bool? isValid)
        {
            //var response = new ApiResponse<IEnumerable<MerchantRemittanceFee>>();
            var merchantRemitFees = _unitOfWork.GetRepository<MerchantRemitProductFee>().GetAllRelatedEntity().AsQueryable();

            var merchantProductList = _unitOfWork.GetRepository<MerchantProduct>().GetAllRelatedEntity().AsQueryable();
            // Apply filters
            //if (merchantId.HasValue)
            //    merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantId == merchantId.Value);
            if (merchantProductId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantProductId == merchantProductId.Value);
            if (remittanceFeeId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.Id == remittanceFeeId.Value);
            if (serviceCategoryId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantProduct.ServiceCategoryId == serviceCategoryId.Value);
            if (instrumentId.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.MerchantProduct.InstrumentId == instrumentId.Value);
            if (sendMinLimit.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.SendMinLimit >= sendMinLimit.Value);
            if (receiveMinLimit.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.ReceiveMinLimit <= receiveMinLimit.Value);
            if (status.HasValue)
                merchantRemitFees = merchantRemitFees.Where(mp => mp.Status == status.Value);
            //if (isValid.HasValue)
            //    merchantRemitFees = merchantRemitFees.Where(mp => mp.va == status.Value);

            // Filter by required path parameters
            merchantRemitFees = merchantRemitFees
                .Where(mp => mp.SendCountry3Iso == sendCountry)
                .Where(mp => mp.ReceiveCountry3Iso == receiveCountry)
                .Where(mp => mp.SendCurrency == sendCurrency)
                .Where(mp => mp.ReceiveCurrency == receiveCurrency)
                .Where(mp => mp.MerchantId == merchantId);

            var innerData = (from merchantRemitFee in merchantRemitFees
                             join mp in merchantProductList
                             on merchantRemitFee.MerchantProductId equals mp.Id
                             select new MerchantRemittanceFee
                             {
                                 Id = merchantRemitFee.Id,
                                 MerchantId = merchantRemitFee.MerchantId,
                                 MerchantName = merchantRemitFee.Merchant.MerchantName,
                                 ServiceCategoryId = merchantRemitFee.MerchantProduct.ServiceCategoryId,
                                 ServiceCategoryName = mp.ServiceCategory.ServCategoryName,
                                 PayoutInstrumentId = mp.Instrument.Id,
                                 PayoutInstrumentName = mp.Instrument.InstrumentName,
                                 ProductId = merchantRemitFee.MerchantProduct.ProductId,
                                 ProductName = mp.Product.ProductName,
                                 FeesName = merchantRemitFee.FeesName,
                                 FeesCurrency = merchantRemitFee.FeesCur,
                                 Fees = merchantRemitFee.Fees,
                                 PromoFees = merchantRemitFee.PromoFees,
                                 MerchantProductId = merchantRemitFee.MerchantProductId,
                                 SendCountry3Iso = merchantRemitFee.SendCountry3Iso,
                                 ReceiveCountry3Iso = merchantRemitFee.ReceiveCountry3Iso,
                                 SendCurrency = merchantRemitFee.SendCurrency,
                                 ReceiveCurrency = merchantRemitFee.ReceiveCurrency,
                                 SendMinLimit = merchantRemitFee.SendMinLimit,
                                 SendMaxLimit = merchantRemitFee.SendMaxLimit,
                                 ReceiveMinLimit = merchantRemitFee.ReceiveMinLimit,
                                 ReceiveMaxLimit = merchantRemitFee.ReceiveMaxLimit,
                                 ValidityExpiry = merchantRemitFee.ValidityExpiry,
                                 Status = merchantRemitFee.Status

                             }).ToList();


            if (innerData.Count > 0)
                return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Success = true, Message = "merchant product remmitance fee record fetch successfully", Data = innerData };
            return new ApiResponse<IEnumerable<MerchantRemittanceFee>>() { Success = false, Message = "merchant product remmitance fee fetch failed" };


        }


        public async Task<ApiResponse<MerchantRemittanceFee>> UpdateMerchantRemitFee(UpdateMerchantRemitProductFeeRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<MerchantRemittanceFee>() { Success = false, Message = "merchant product remmitance fee update failed" };
                var isAuthenticatedMerchant = await _permissionService.CheckMerchantPermission(model.MerchantId);
                if (!isAuthenticatedMerchant)
                {
                    return new ApiResponse<MerchantRemittanceFee>() { Success = false, Message = "Invalid/Unauthorized merchant" };

                }

                var checkData = await _unitOfWork.GetRepository<MerchantRemitProductFee>().GetById(model.Id);
                if (checkData == null)
                    return new ApiResponse<MerchantRemittanceFee>() { Success = false, Message = "merchant product remmitance fee update failed" };

                var requestData = _mapper.Map(model, checkData);
                await _unitOfWork.GetRepository<MerchantRemitProductFee>().Upsert(requestData);
                await _unitOfWork.SaveChangesAsync();
                var merchantRemitFee = await _unitOfWork.GetRepository<MerchantRemitProductFee>().GetByPrimaryKeyWithRelatedEntitiesAsync<int>(requestData.Id);

                var data = new MerchantRemittanceFee
                {
                    Id = merchantRemitFee.Id,
                    MerchantId = merchantRemitFee.MerchantId,
                    MerchantName = merchantRemitFee.Merchant.MerchantName,
                    ServiceCategoryId = merchantRemitFee.MerchantProduct.ServiceCategoryId,
                    ServiceCategoryName = merchantRemitFee.MerchantProduct.ServiceCategory.ServCategoryName,
                    PayoutInstrumentId = merchantRemitFee.MerchantProduct.Instrument.Id,
                    PayoutInstrumentName = merchantRemitFee.MerchantProduct.Instrument.InstrumentName,
                    ProductId = merchantRemitFee.MerchantProduct.ProductId,
                    ProductName = merchantRemitFee.MerchantProduct.Product.ProductName,
                    FeesName = merchantRemitFee.FeesName,
                    FeesCurrency = merchantRemitFee.FeesCur,
                    Fees = merchantRemitFee.Fees,
                    PromoFees = merchantRemitFee.PromoFees,
                    MerchantProductId = merchantRemitFee.MerchantProductId,
                    SendCountry3Iso = merchantRemitFee.SendCountry3Iso,
                    ReceiveCountry3Iso = merchantRemitFee.ReceiveCountry3Iso,
                    SendCurrency = merchantRemitFee.SendCurrency,
                    ReceiveCurrency = merchantRemitFee.ReceiveCurrency,
                    SendMinLimit = merchantRemitFee.SendMinLimit,
                    SendMaxLimit = merchantRemitFee.SendMaxLimit,
                    ReceiveMinLimit = merchantRemitFee.ReceiveMinLimit,
                    ReceiveMaxLimit = merchantRemitFee.ReceiveMaxLimit,
                    ValidityExpiry = merchantRemitFee.ValidityExpiry,
                    PayInInstrumentId = merchantRemitFee.PayInInstrumentId,
                    VariableFee = merchantRemitFee.VariableFee,
                    PayInInstrumentName = merchantRemitFee.Instruments.InstrumentName.ToString(),
                    Status = merchantRemitFee.Status
                };

                return new ApiResponse<MerchantRemittanceFee>() { Success = true, Message = "Merchant Product Remittance Fee record updated successfully!", Data = data };

            }
            catch (Exception ex)
            {
                throw new NotImplementedException($"merchant product remmitance fee update failed {ex.Message}");
            }
        }



    }
}
