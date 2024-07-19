using Fincompare.Application.Repositories;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.ServiceCategoriesRequest.ServiceCategoriesViewModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategory _serviceCategory;

        public ServiceCategoryController(IServiceCategory serviceCategory)
        {
            _serviceCategory = serviceCategory;
        }
        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("add-service-categories")]
        public async Task<IActionResult> CreateServiceCategories(CreateServiceCategoriesRequest model)
        {
            var response = await _serviceCategory.CreateServiceCategories(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-service-categories")]
        public async Task<IActionResult> FetchAllServiceCategories(int? idServCategory, string? countryIso3, bool? status)
        {
            var response = await _serviceCategory.FetchAllServiceCategories(idServCategory, countryIso3, status);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("update-service-categories")]
        public async Task<IActionResult> UpdateServiceCategories(UpdateServiceCategoriesRequest model)
        {
            var response = await _serviceCategory.UpdateServiceCategories(model);
            return Ok(response);
        }
    }
}
