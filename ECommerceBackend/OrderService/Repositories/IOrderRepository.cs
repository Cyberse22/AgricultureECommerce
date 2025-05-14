using OrderService.Data;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<Order> GetOrderByIdAsync(string orderId);
        Task<bool> CancelOrderAsync(string orderId);
        Task CreateOrderAsync(Order order);
        Task<int> GetTodayOrderCountAsync(DateTime today);
    }
}
