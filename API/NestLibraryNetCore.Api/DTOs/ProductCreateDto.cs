﻿using NestLibraryNetCore.Api.Models;

namespace NestLibraryNetCore.Api.DTOs
{
    public record ProductCreateDto(  string? Name,  decimal Price,int Stock,ProductFeatureDto? Feature )
    {
       
    }
}
