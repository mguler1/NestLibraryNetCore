using Nest;
using NestLibraryNetCore.Api.Models;

namespace NestLibraryNetCore.Api.DTOs
{
    public record ProductDto(string? Id, string? Name, decimal Price,int Stock,ProductFeatureDto? Feature)
    { }
      
}
