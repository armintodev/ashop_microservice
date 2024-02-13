using System.Text.Json;
using System.Text.Json.Serialization;
using Basket.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Repositories;

public class BasketRepository(IDistributedCache cache) : IBasketRepository
{
    private readonly IDistributedCache _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));

    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);

        if (string.IsNullOrEmpty(basket))
            return null;

        return JsonSerializer.Deserialize<ShoppingCart>(basket) ??
               throw new InvalidOperationException("cannot deserialize Basket");
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
        await _redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));

        return await GetBasket(basket.UserName);
    }

    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }
}