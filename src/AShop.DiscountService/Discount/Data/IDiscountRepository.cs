using Discount.Data.Entities;

namespace Discount.Data;

public interface IDiscountRepository
{
    Task<Coupon?> GetDiscount(string productName, CancellationToken ct);
    Task CreateDiscount(Coupon coupon, CancellationToken ct);
    void UpdateDiscount(Coupon coupon);
    Task DeleteDiscount(string productName, CancellationToken ct);
}