using OrderService.Models;

namespace OrderService.Services;

public interface IOrderService
{
    Task<List<OrderModel>> GetOrdersByUserIdAsync(string userId);
    Task<OrderModel> GetOrderByIdAsync(string orderId);
    Task<bool> CancelOrderAsync(string orderId);
}