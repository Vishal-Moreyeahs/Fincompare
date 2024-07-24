using Fincompare.Application.Repositories;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.ProductRequests.ProductRequestViewModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> CreateProduct(CreateProductRequest model)
        {
            var response = await _productService.CreateProduct(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest model)
        {
            var response = await _productService.UpdateProduct(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-product")]
        public async Task<IActionResult> GetAllProduct(string? countryIso3, int? idProduct, int? idServCategory, bool? status)
        {
            var response = await _productService.GetAllProduct(countryIso3, idProduct, idServCategory, status);
            return Ok(response);
        }

        //[HttpGet]
        //[Route("fetch-id-product")]
        //public async Task<IActionResult> GetProductById(int id)
        //{
        //    var response = await _productService.GetProductById(id);
        //    return Ok(response);
        //}
    }
}
