using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantCompaignController : ControllerBase
    {
        private readonly IMerchantCompaignServices _merchantCompaignService;

        public MerchantCompaignController(IMerchantCompaignServices merchantCompaignServices)
        {
            _merchantCompaignService = merchantCompaignServices;
        }

        [HttpGet]
        public IActionResult GetMerchantCampaigns(
            [FromQuery] int? MerchantCampaignId,
            [FromQuery] int? MerchantID,
            [FromQuery] string sendCountry,
            [FromQuery] string receiveCountry,
            //[FromQuery] string sendCurrency,
            //[FromQuery] string receiveCurrency,
            [FromQuery] int? MerchantProductID,
            [FromQuery] int? serviceCategoryId,
            [FromQuery] int? instrumentId,
            [FromQuery] DateTime? dateValidity)
        //[FromQuery] decimal? SendMinLimit,
        //[FromQuery] decimal? ReceiveMinLimit)
        {
            // Your logic to get merchant campaigns based on the parameters
            var result = _merchantCompaignService.GetMerchantCampaigns(
                MerchantCampaignId,
                MerchantID,
                sendCountry,
                receiveCountry,
                //sendCurrency,
                //receiveCurrency,
                MerchantProductID,
                serviceCategoryId,
                instrumentId,
                dateValidity
            //SendMinLimit,
            //ReceiveMinLimit
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
