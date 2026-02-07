using ShopManagement.Core.Entities;

namespace ShopManagement.Core.Interfaces;

/// <summary>
/// Customer-specific repository interface
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByEmailAsync(string email);
    Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync();
    Task<bool> EmailExistsAsync(string email);
}