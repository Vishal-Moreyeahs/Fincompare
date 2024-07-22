using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateCardController : ControllerBase
    {
        private readonly IRateCardServices _rateCardService;

        public RateCardController(IRateCardServices rateCardServices)
        {
            _rateCardService = rateCardServices;
        }
    }
}
