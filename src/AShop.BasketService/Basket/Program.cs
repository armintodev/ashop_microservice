using Basket.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ArgumentException.ThrowIfNullOrEmpty(builder.Configuration.GetConnectionString("redis"));

builder.Services.AddControllers();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1"
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1"); });

app.MapControllers();

app.Run();