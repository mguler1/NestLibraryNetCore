using Microsoft.AspNetCore.Mvc;
using NestLibraryNetCore.Api.DTOs;
using NestLibraryNetCore.Api.Services;

namespace NestLibraryNetCore.Api.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto request)
        {
            return CreateActionResult(await _productService.SaveAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return CreateActionResult(await _productService.GetAllAsync());
        }
    }
}