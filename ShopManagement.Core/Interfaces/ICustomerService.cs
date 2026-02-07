using ShopManagement.Core.Entities;

namespace ShopManagement.Core.Interfaces;

/// <summary>
/// Customer service interface for business logic operations
/// Students need to implement this interface as part of the assignment
/// </summary>
public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<Customer> UpdateCustomerAsync(Customer customer);
    Task<bool> DeleteCustomerAsync(int id);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync();
    Task<decimal> GetCustomerTotalOrderValueAsync(int customerId);
}