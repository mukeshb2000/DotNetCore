using ShopManagement.Core.Entities;

namespace ShopManagement.Core.Interfaces;

/// <summary>
/// Product-specific repository interface demonstrating Interface Segregation
/// Extends the generic repository with product-specific operations
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetInStockAsync();
    Task<IEnumerable<Product>> GetLowStockAsync(int threshold = 10);
    Task<IEnumerable<Product>> SearchByNameAsync(string name);
    Task<bool> UpdateStockAsync(int productId, int quantity);
}