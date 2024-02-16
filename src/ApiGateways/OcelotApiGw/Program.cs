using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("ocelot.json", true, true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();

builder.Services.AddOcelot(configuration)
    .AddCacheManager(s => { s.WithDictionaryHandle(); });

var app = builder.Build();

app.UseRouting();

app.UseOcelot();

app.Run();