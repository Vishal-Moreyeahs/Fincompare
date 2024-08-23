using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Application.Response.AssetMasterResponse;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Response.ProductResponse.ProductResponseBaseClass;

namespace Fincompare.Application.Services
{
    public class AssetMasterService : IAssetMasterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssetMasterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<AssetMasterViewModel>>> GetAllAssetMaster(string countryIso3, int? assetMasterId, bool? status)
        {
            var getData = await _unitOfWork.GetRepository<AssetsMaster>().GetAll();
            if (!string.IsNullOrEmpty(countryIso3))
            {
                getData = getData.Where(x => x.Country3Iso == countryIso3).ToList();
            }
            if (assetMasterId.HasValue)
            {
                getData = getData.Where(x => x.Id == assetMasterId.Value).ToList();
            }
            if (status.HasValue)
            {
                getData = getData.Where(x => x.Status == status).ToList();
            }
            var getAssetMasterList = _mapper.Map<IEnumerable<AssetMasterViewModel>>(getData).ToList();
            if (getAssetMasterList.Count == 0 || getAssetMasterList == null)
                return new ApiResponse<IEnumerable<AssetMasterViewModel>>() { Success = false, Message = "asset master fetch failed" };
            return new ApiResponse<IEnumerable<AssetMasterViewModel>>() { Success = true, Message = "asset master record fetched successfully", Data = getAssetMasterList };

        }
    }
}
