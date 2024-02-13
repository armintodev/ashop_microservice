using Domain.Entities;

namespace Persistence.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);
    Task<Product> GetProduct(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductByCategory(string categoryName, CancellationToken cancellationToken);
    Task Create(Product product, CancellationToken cancellationToken);
    Task<bool> Update(Product product, CancellationToken cancellationToken);
    Task<bool> Delete(string id, CancellationToken cancellationToken);
}