﻿using Nest;
using NestLibraryNetCore.Api.DTOs;
using NestLibraryNetCore.Api.Models;
using NestLibraryNetCore.Api.Repository;
using System.Collections.Immutable;
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
                return ResponseDto<ProductDto>.Fail(new List<string> { "Error" }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<ProductDto>.Success(response.CreateDto(), HttpStatusCode.Created);

        }
        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productListDto = new List<ProductDto>();

            foreach (var product in products)
            {
                if (product.Feature == null)
                {
                    productListDto.Add(new ProductDto(product.Id, product.Name, product.Price, product.Stock, null));
                }
                else
                {
                    productListDto.Add(new ProductDto(product.Id, product.Name, product.Price, product.Stock, new ProductFeatureDto(product.Feature.Width, product.Feature.Height, product.Feature.Color)));
                }
            }
            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> { "Product not found" }, HttpStatusCode.NotFound);
            }
            return ResponseDto<ProductDto>.Success(product.CreateDto(), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var isSuccess = await _productRepository.UpdateAsync(productUpdateDto);
            if (!isSuccess)
            {
                return ResponseDto<bool>.Fail(new List<string> { "Product not found" }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var response = await _productRepository.DeleteAsync(id);
            if (!response)
            {
                return ResponseDto<bool>.Fail(new List<string> { "Error deleting product" }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }
    }
}