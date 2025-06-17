using Nest;
using NestLibraryNetCore.Api.DTOs;
using NestLibraryNetCore.Api.Models;
using System.Collections.Immutable;

namespace NestLibraryNetCore.Api.Repository
{
    public class ProductRepository
    {
        private readonly ElasticClient _client;
        private const string IndexName = "products";
        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product product)
        {
            product.Created = DateTime.UtcNow;

            var response = await _client.IndexAsync(product, x => x.Index(IndexName).Id(Guid.NewGuid().ToString()));

            if (!response.IsValid)
            {
                return null;
            }
            product.Id = response.Id;
            return product;
        }

        public async Task<IImmutableList<Product>> GetAllAsync()
        {
            var result = await _client.SearchAsync<Product>(s => s
                .Index(IndexName)
                .Query(q => q.MatchAll()));
            foreach (var hit in result.Hits)
            {
                hit.Source.Id = hit.Id; // Assign the Id from the hit to the source object
            }
            return result.Documents.ToImmutableList();
        }

        public async Task<Product> GetById(string id)
        {
            var response = await _client.GetAsync<Product>(id, g => g.Index(IndexName));
            if (!response.IsValid || !response.Found)
            {
                return null!;
            }
            response.Source.Id = response.Id;
            return response.Source;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var response = await _client.UpdateAsync<Product, ProductUpdateDto>(productUpdateDto.Id, u => u
                .Index(IndexName)
                .Doc(productUpdateDto));
            return response.IsValid && response.Result == Result.Updated;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync<Product>(id, d => d.Index(IndexName));
            return response.IsValid && response.Result == Result.Deleted;
        }
    }
}