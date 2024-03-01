using Discount.Data;
using Discount.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ArgumentException.ThrowIfNullOrEmpty(builder.Configuration.GetConnectionString("postgres"));

builder.Services.AddControllers();

builder.Services.AddDbContext<DiscountContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

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

if (builder.Environment.IsDevelopment())
    await app.Seed();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1"); });

app.MapControllers();

app.Run();