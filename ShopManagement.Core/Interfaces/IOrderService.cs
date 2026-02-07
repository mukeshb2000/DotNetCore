using ShopManagement.Core.Entities;

namespace ShopManagement.Core.Interfaces;

/// <summary>
/// Order service interface for business logic operations
/// Students need to implement this interface as part of the assignment
/// </summary>
public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(int id);
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
    Task<Order> AddItemToOrderAsync(int orderId, int productId, int quantity);
    Task<Order> RemoveItemFromOrderAsync(int orderId, int orderItemId);
    Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatus status);
    Task<bool> CanFulfillOrderAsync(int orderId);
}