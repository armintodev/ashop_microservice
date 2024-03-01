using Microsoft.EntityFrameworkCore;

namespace Discount.Data.Seed;

public static class SeedDiscountData
{
    public static async Task Seed(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var discountContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();

        var migrations = await discountContext.Database.GetPendingMigrationsAsync();

        Console.WriteLine("Pending Migrations: {0}", migrations.Select(_ => _));

        Console.WriteLine("Database has Created: {0}", await discountContext.Database.EnsureCreatedAsync());
    }
}