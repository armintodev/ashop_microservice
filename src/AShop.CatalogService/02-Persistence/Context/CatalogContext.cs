using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Context;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("CatalogDb");

        Products = database.GetCollection<Product>("Products");
    }

    public IMongoCollection<Product> Products { get; }
}