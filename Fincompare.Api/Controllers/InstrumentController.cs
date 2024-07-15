using Fincompare.Application.Repositories;
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

        [HttpPost]
        [Route("add-instrument")]
        public async Task<IActionResult> CreateInstrument(CreateInstrumentRequest model)
        {
            var response = await _instrumentService.CreateInstrument(model);
            return Ok(response);
        }

        [HttpPost]
        [Route("update-instrument")]
        public async Task<IActionResult> UpdateInstrument(UpdateInstrumentRequest model)
        {
            var response = await _instrumentService.UpdateInstrument(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("getall-instrument")]
        public async Task<IActionResult> GetAllInstrument()
        {
            var response = await _instrumentService.GetAllInstrument();
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
