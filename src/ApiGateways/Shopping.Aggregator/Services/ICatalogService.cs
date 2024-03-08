using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public interface ICatalogService
{
    Task<IEnumerable<CatalogModel>> GetCatalog();
    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    Task<CatalogModel> GetCatalog(string id);
}

public interface IBasketService
{
    Task<BasketModel> GetBasket(string userName);
}

public class BasketService(HttpClient client) : IBasketService
{
    public async Task<BasketModel> GetBasket(string userName)
    {
        var response = await client.GetAsync($"/api/v1/Basket/{userName}");
        return await response.ReadContentAs<BasketModel>();
    }
}