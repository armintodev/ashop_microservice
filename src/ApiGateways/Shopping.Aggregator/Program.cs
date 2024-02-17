using Shopping.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);

ArgumentException.ThrowIfNullOrWhiteSpace(builder.Configuration["ApiSettings:CatalogUrl"]);
ArgumentException.ThrowIfNullOrWhiteSpace(builder.Configuration["ApiSettings:BasketUrl"]);

builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiSettings:CatalogUrl"]!));
// builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
//     c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BasketUrl"]));

var app = builder.Build();


app.Run();