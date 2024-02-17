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

// public interface IOrderService
// {
//     Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
// }

// public class OrderService : IOrderService
// {
//     private readonly HttpClient _client;
//     public OrderService(HttpClient client)
//     {
//         _client = client ?? throw new ArgumentNullException(nameof(client));
//     }
//     public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
//     {
//         var response = await _client.GetAsync($”/ api / v1 / Order /{ userName}”);
//         return await response.ReadContentAs<List<OrderResponseModel>>();
//     }
// }