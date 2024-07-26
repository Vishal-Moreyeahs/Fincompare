using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.GroupMerchantRequest.GroupMerchantBaseModel;
using static Fincompare.Application.Response.GroupMerchantResponse.GroupMerchantViewResponse;

namespace Fincompare.Application.Services
{
    public class GroupMerchantService : IGroupMerchantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupMerchantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetAllGroupMerchantResponse>> AddGroupMerchant(AddGroupMerchantRequestClass model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<GetAllGroupMerchantResponse>()
                    {
                        Success = false,
                        Message = "merchant group creation failed"
                    };
                var createdData = _mapper.Map<GroupMerchant>(model);
                await _unitOfWork.GetRepository<GroupMerchant>().Add(createdData);
                await _unitOfWork.SaveChangesAsync();

                var data = _mapper.Map<GetAllGroupMerchantResponse>(createdData);
                return new ApiResponse<GetAllGroupMerchantResponse>() { Success = true, Message = "group merchant record created successfully", Data = data };

                //var response = new ApiResponse<string>()
                //{
                //    Status = true,
                //    Message = "Merchant Group Created Successfully"
                //};
                //return response;
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"merchant group creation failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<UpdateGroupMerchantRequestClass>> UpdateGroupMerchant(UpdateGroupMerchantRequestClass model)
        {
            try
            {
                var checkGroup = await _unitOfWork.GetRepository<GroupMerchant>().GetById(model.Id);
                if (checkGroup == null)
                    return new ApiResponse<UpdateGroupMerchantRequestClass>() { Success = false, Message = "group merchant update failed" };
                var groupData = _mapper.Map(model, checkGroup);
                await _unitOfWork.GetRepository<GroupMerchant>().Upsert(groupData);
                await _unitOfWork.SaveChangesAsync();
                var data = _mapper.Map<UpdateGroupMerchantRequestClass>(model);
                return new ApiResponse<UpdateGroupMerchantRequestClass>() { Success = true, Message = "group merchant record updated successfully", Data = data };

            }
            catch (Exception ex)
            {

                throw new ApplicationException($"group merchant update failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<GetAllGroupMerchantResponse>>> GetAllGroupMerchant(int? groupMerchantId, string? countryIso3, bool? status)
        {
            try
            {
                var getAllMerchantGroup = await _unitOfWork.GetRepository<GroupMerchant>().GetAll();


                if (groupMerchantId.HasValue)
                    getAllMerchantGroup = getAllMerchantGroup.Where(mp => mp.Id == groupMerchantId.Value);
                if (!string.IsNullOrEmpty(countryIso3))
                    getAllMerchantGroup = getAllMerchantGroup.Where(mp => mp.Country3Iso == countryIso3);
                if (status.HasValue)
                    getAllMerchantGroup = getAllMerchantGroup.Where(mp => mp.Status == status.Value);


                var getData = getAllMerchantGroup
                    .Select(x => new GetAllGroupMerchantResponse
                    {
                        Id = x.Id,
                        GroupMerchantName = x.GroupMerchantName,
                        GroupMerchantShortName = x.GroupMerchantShortName,
                        GroupPh1 = x.GroupPh1,
                        GroupPh2 = x.GroupPh2,
                        GroupEm1 = x.GroupEm1,
                        GroupEm2 = x.GroupEm2,
                        GroupCsph = x.GroupCsph,
                        GroupCsem = x.GroupCsem,
                        Country3Iso = x.Country3Iso,
                        Status = x.Status
                    }).ToList();
                if (getData.Count == 0)
                    return new ApiResponse<IEnumerable<GetAllGroupMerchantResponse>>() { Success = false, Message = "group merchant fetch failed" };
                return new ApiResponse<IEnumerable<GetAllGroupMerchantResponse>>() { Success = true, Message = "group merchant record fetched successfully", Data = getData };
            }
            catch (Exception ex)
            {

                throw new ApplicationException($" group merchant fetch failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<GetAllGroupMerchantResponse>> GetByIdGroupMerchant(int id)
        {
            try
            {
                var getData = await _unitOfWork.GetRepository<GroupMerchant>().GetById(id);
                var getGroupData = _mapper.Map<GetAllGroupMerchantResponse>(getData);
                if (getGroupData == null)
                    return new ApiResponse<GetAllGroupMerchantResponse>() { Success = false, Message = "Group Not Found !" };
                return new ApiResponse<GetAllGroupMerchantResponse>() { Success = true, Message = "Merchant Group Found !", Data = getGroupData };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

    }
}
