using ShopManagement.Core.Entities;

namespace ShopManagement.Core.Interfaces;

/// <summary>
/// Product service interface demonstrating Service Layer pattern
/// Contains business logic and orchestrates repository operations
/// </summary>
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetInStockProductsAsync();
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task<bool> UpdateProductStockAsync(int productId, int quantity);
    Task<bool> IsProductInStockAsync(int productId, int requiredQuantity);
}