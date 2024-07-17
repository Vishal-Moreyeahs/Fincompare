using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.MerchantRemitProductFeeRequests.MerchantRemitProductFeeRequestViewModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantRemitFeeController : ControllerBase
    {
        private readonly IMerchantRemitFee _merchantRemitFee;

        public MerchantRemitFeeController(IMerchantRemitFee merchantRemitFee)
        {
            _merchantRemitFee = merchantRemitFee;
        }

        [HttpPost]
        [Route("add-merchantremit-fee")]
        public async Task<IActionResult> AddMerchantRemitFee(CreateMerchantRemitProductFeeRequest model)
        {
            var response = await _merchantRemitFee.AddMerchantRemitFee(model);
            return Ok(response);
        }


        [HttpPost]
        [Route("update-merchantremit-fee")]
        public async Task<IActionResult> UpdateMerchantRemitFee(UpdateMerchantRemitProductFeeRequest model)
        {
            var response = await _merchantRemitFee.UpdateMerchantRemitFee(model);
            return Ok(response);
        }
    }
}
