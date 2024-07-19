using Fincompare.Application.Repositories;
using Fincompare.Application.Request.StateRequest;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateServices _stateService;

        public StateController(IStateServices stateService)
        {
            _stateService = stateService;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("add-state")]
        public async Task<IActionResult> AddState(AddStateRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _stateService.AddState(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("update-state")]
        public async Task<IActionResult> UpdateState(UpdateStateRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _stateService.UpdateState(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-all-states")]
        public async Task<IActionResult> GetAllState(string? countryIso3, int? stateId, bool? status)
        {
            var response = await _stateService.GetAllState(countryIso3, stateId, status);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-state-by-currency3iso")]
        public async Task<IActionResult> GetStateByCountryIso(string currency3iso)
        {
            var response = await _stateService.GetStateByCountryIso(currency3iso);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-by-stateid")]
        public async Task<IActionResult> GetByStateId(int stateId)
        {
            var response = await _stateService.GetByStateId(stateId);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpDelete]
        [Route("delete-state")]
        public async Task<IActionResult> DeleteState(int stateId)
        {
            var response = await _stateService.DeleteState(stateId);
            return Ok(response);
        }
    }
}
