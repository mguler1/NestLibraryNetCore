using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NestLibraryNetCore.Api.DTOs;
using NestLibraryNetCore.Api.Services;

namespace NestLibraryNetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task <IActionResult> Save (ProductCreateDto request)
        {
            return Ok(await _productService.SaveAsync(request));
        }
    }
}
