using OrderService.Data;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task <IEnumerable<Order>> GetAllOrdersAsync();
        Task <Order?> GetOrderByIdAsync(string orderId);
        Task <Order> CreateOrderAsync(Order order);
        Task SaveChangesAsync();
        Task <IEnumerable<Order>> GetOrdersByDateAsync(DateTime date);
        Task<int> GetTodayOrderCountAsync(DateTime date);

    }
}
