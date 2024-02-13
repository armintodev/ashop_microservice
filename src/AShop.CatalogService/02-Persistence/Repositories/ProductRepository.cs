using Domain.Entities;

using MongoDB.Driver;

using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
    }

    public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken)
    {
        return await _catalogContext
            .Products
            .Find(p => true)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product> GetProduct(string id, CancellationToken cancellationToken)
    {
        return await _catalogContext
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name, CancellationToken cancellationToken)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
        return await _catalogContext
            .Products
            .Find(filter)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName, CancellationToken cancellationToken)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
        return await _catalogContext
            .Products
            .Find(filter)
            .ToListAsync(cancellationToken);
    }

    public async Task Create(Product product, CancellationToken cancellationToken)
    {
        await _catalogContext.Products.InsertOneAsync(product, null, cancellationToken);
    }

    public async Task<bool> Update(Product product, CancellationToken cancellationToken)
    {
        var updateResult = await _catalogContext
            .Products
            .ReplaceOneAsync(filter: g =>
                g.Id == product.Id,
                replacement: product,
                cancellationToken: cancellationToken);

        return updateResult.IsAcknowledged
               && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id, CancellationToken cancellationToken)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _catalogContext
            .Products
            .DeleteOneAsync(filter, cancellationToken);

        return deleteResult.IsAcknowledged
               && deleteResult.DeletedCount > 0;
    }
}