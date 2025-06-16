using NestLibraryNetCore.Api.DTOs;
using NestLibraryNetCore.Api.Models;
using NestLibraryNetCore.Api.Repository;
using System.Net;

namespace NestLibraryNetCore.Api.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
            var product = new Product()
            {
                Created = DateTime.Now,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                Feature = new ProductFeature()
                {
                    Color = request.Feature!.Color,
                    Height = request.Feature.Height,
                    Width = request.Feature.Width,
                }
            };
            var response = await _productRepository.SaveAsync(product);
            if (response == null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> {"Error"},HttpStatusCode.InternalServerError);
            }
            return ResponseDto<ProductDto>.Success(response.CreateDto(),HttpStatusCode.Created);

        }
    }
}