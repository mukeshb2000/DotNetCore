using Microsoft.EntityFrameworkCore;
using ShopManagement.Core.Entities;
using ShopManagement.Core.Interfaces;
using ShopManagement.Infrastructure.Data;

namespace ShopManagement.Infrastructure.Repositories;

/// <summary>
/// Product repository implementation demonstrating specialized repository
/// Inherits from generic repository and adds product-specific operations
/// </summary>
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ShopDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }

    public override async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetInStockAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.StockQuantity > 0 && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockAsync(int threshold = 10)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.StockQuantity <= threshold && p.StockQuantity > 0 && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return await GetAllAsync();

        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.Name.Contains(name) && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantity)
    {
        var product = await GetByIdAsync(productId);
        if (product == null)
            return false;

        try
        {
            product.UpdateStock(quantity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}