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
        [Route("add-instrument")]
        public async Task<IActionResult> CreateInstrument(CreateInstrumentRequest model)
        {
            var response = await _instrumentService.CreateInstrument(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("update-instrument")]
        public async Task<IActionResult> UpdateInstrument(UpdateInstrumentRequest model)
        {
            var response = await _instrumentService.UpdateInstrument(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("getall-instrument")]
        public async Task<IActionResult> GetAllInstrument(int? idInstrument, bool? status)
        {
            var response = await _instrumentService.GetAllInstrument(idInstrument, status);
            return Ok(response);
        }

        [HttpGet]
        [Route("getby-id-instrument")]
        public async Task<IActionResult> GetInstrumentById(int id)
        {
            var response = await _instrumentService.GetInstrumentById(id);
            return Ok(response);
        }
    }
}
