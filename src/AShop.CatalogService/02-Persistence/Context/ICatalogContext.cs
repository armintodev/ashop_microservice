using Domain.Entities;

using MongoDB.Driver;

namespace Persistence.Context;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}