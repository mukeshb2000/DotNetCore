using Microsoft.EntityFrameworkCore;
using ShopManagement.Core.Entities;
using ShopManagement.Core.Interfaces;
using ShopManagement.Infrastructure.Data;

namespace ShopManagement.Infrastructure.Repositories;

/// <summary>
/// Customer repository implementation
/// </summary>
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ShopDbContext context) : base(context)
    {
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        return await _dbSet
            .FirstOrDefaultAsync(c => c.Email == email && !c.IsDeleted);
    }

    public async Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync()
    {
        return await _dbSet
            .Include(c => c.Orders)
            .Where(c => !c.IsDeleted && c.Orders.Any())
            .ToListAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return await _dbSet
            .AnyAsync(c => c.Email == email && !c.IsDeleted);
    }
}