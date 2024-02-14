using Discount.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Data;

public class DiscountContext : DbContext
{
    public DiscountContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }
}