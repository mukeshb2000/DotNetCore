using ShopManagement.Core.Entities;

namespace ShopManagement.Core.Interfaces;

/// <summary>
/// Order-specific repository interface
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByCustomerAsync(int customerId);
    Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
    Task<IEnumerable<Order>> GetOrdersWithItemsAsync();
    Task<Order?> GetOrderWithItemsAsync(int orderId);
}