namespace NestLibraryNetCore.Api.DTOs
{
    public record ProductUpdateDto(string Id,string? Name, decimal Price, int Stock, ProductFeatureDto? Feature)
    {
    }
}
