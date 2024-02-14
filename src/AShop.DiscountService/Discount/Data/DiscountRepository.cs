using Discount.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Data;

public class DiscountRepository(DiscountContext context) : IDiscountRepository
{
    public Task<Coupon?> GetDiscount(string productName, CancellationToken ct)
    {
        return context.Coupons.Where(c => c.ProductName == productName).FirstOrDefaultAsync(ct);
    }

    public async Task CreateDiscount(Coupon coupon, CancellationToken ct)
    {
        await context.Coupons.AddAsync(coupon, ct);
        await context.SaveChangesAsync(ct);
    }

    public void UpdateDiscount(Coupon coupon)
    {
        context.Coupons.Update(coupon);
        context.SaveChanges();
    }

    public async Task DeleteDiscount(string productName, CancellationToken ct)
    {
        await context.Coupons.Where(c => c.ProductName == productName).ExecuteDeleteAsync(ct);
    }
}