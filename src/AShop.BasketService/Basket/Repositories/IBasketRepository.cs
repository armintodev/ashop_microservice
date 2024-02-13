using Basket.Entities;

namespace Basket.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasket(string userName);
    Task<ShoppingCart?> UpdateBasket(ShoppingCart basket);
    Task DeleteBasket(string userName);
}