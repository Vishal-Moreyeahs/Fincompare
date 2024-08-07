using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.InstrumentRequest.InstrumentRequestBaseModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : ControllerBase
    {
        private readonly IInstrumentService _instrumentService;

        public InstrumentController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [ValidateModelState]
        [Route("add-instrument")]
        public async Task<IActionResult> CreateInstrument(CreateInstrumentRequest model)
        {
            var response = await _instrumentService.CreateInstrument(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPut]
        [ValidateModelState]
        [Route("update-instrument")]
        public async Task<IActionResult> UpdateInstrument(UpdateInstrumentRequest model)
        {
            var response = await _instrumentService.UpdateInstrument(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-instrument")]
        public async Task<IActionResult> GetAllInstrument(int? idInstrument, bool? status, string? instrumentType/*, string? countryIso3*/)
        {
            var response = await _instrumentService.GetAllInstrument(idInstrument, status, instrumentType/*, countryIso3*/);
            return Ok(response);
        }

        //[HttpGet]
        //[Route("getby-id-instrument")]
        //public async Task<IActionResult> GetInstrumentById(int id)
        //{
        //    var response = await _instrumentService.GetInstrumentById(id);
        //    return Ok(response);
        //}
    }
}
