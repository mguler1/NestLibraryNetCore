
using NestLibraryNetCore.Api.Extensions;
using NestLibraryNetCore.Api.Repository;
using NestLibraryNetCore.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddElasticSearch(builder.Configuration);
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
