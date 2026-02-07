using ShopManagement.Core.Entities;
using ShopManagement.Core.Interfaces;

namespace ShopManagement.Infrastructure.Services;

/// <summary>
/// Product service implementation demonstrating Service Layer pattern
/// Contains business logic and orchestrates repository operations
/// Demonstrates Dependency Injection through constructor injection
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    // Constructor injection demonstrating Dependency Injection
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Product ID must be positive", nameof(id));

        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        // Business logic validation
        await ValidateProductAsync(product);

        return await _productRepository.CreateAsync(product);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (product.Id <= 0)
            throw new ArgumentException("Product ID must be positive", nameof(product));

        // Check if product exists
        var existingProduct = await _productRepository.GetByIdAsync(product.Id);
        if (existingProduct == null)
            throw new InvalidOperationException($"Product with ID {product.Id} not found");

        // Business logic validation
        await ValidateProductAsync(product);

        return await _productRepository.UpdateAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Product ID must be positive", nameof(id));

        // Business logic: Check if product can be deleted
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return false;

        // Additional business logic could be added here
        // e.g., check if product is in any pending orders

        return await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Category ID must be positive", nameof(categoryId));

        return await _productRepository.GetByCategoryAsync(categoryId);
    }

    public async Task<IEnumerable<Product>> GetInStockProductsAsync()
    {
        return await _productRepository.GetInStockAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10)
    {
        if (threshold < 0)
            throw new ArgumentException("Threshold cannot be negative", nameof(threshold));

        return await _productRepository.GetLowStockAsync(threshold);
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllProductsAsync();

        return await _productRepository.SearchByNameAsync(searchTerm);
    }

    public async Task<bool> UpdateProductStockAsync(int productId, int quantity)
    {
        if (productId <= 0)
            throw new ArgumentException("Product ID must be positive", nameof(productId));

        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            return false;

        // Business logic: Validate stock update
        if (product.StockQuantity + quantity < 0)
            throw new InvalidOperationException("Insufficient stock for this operation");

        return await _productRepository.UpdateStockAsync(productId, quantity);
    }

    public async Task<bool> IsProductInStockAsync(int productId, int requiredQuantity)
    {
        if (productId <= 0)
            throw new ArgumentException("Product ID must be positive", nameof(productId));

        if (requiredQuantity <= 0)
            throw new ArgumentException("Required quantity must be positive", nameof(requiredQuantity));

        var product = await _productRepository.GetByIdAsync(productId);
        return product != null && product.StockQuantity >= requiredQuantity;
    }

    // Private method for business logic validation
    private Task ValidateProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required");

        if (product.Price < 0)
            throw new ArgumentException("Product price cannot be negative");

        if (product.StockQuantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative");

        if (product.CategoryId <= 0)
            throw new ArgumentException("Valid category is required");

        // Additional business validations can be added here
        // e.g., check if category exists, validate product name uniqueness, etc.
        
        return Task.CompletedTask;
    }
}