using Microsoft.EntityFrameworkCore;
using OrderService.Data;

namespace OrderService.Repositories.Impl;

public class OrderRepositoryImpl : IOrderRepository
{
    private readonly OrderDbContext _orderDbContextcontext;

    public OrderRepositoryImpl(OrderDbContext orderDbContextcontext)
    {
        _orderDbContextcontext = orderDbContextcontext;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
       return await _orderDbContextcontext.Orders.Include(o => o.Items).ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(string orderId)
    {
        return await _orderDbContextcontext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        _orderDbContextcontext.Orders.Add(order);
        await _orderDbContextcontext.SaveChangesAsync();
        return order;
    }

    public async Task SaveChangesAsync()
    {
        await _orderDbContextcontext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date)
    {
        var start = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        var end = start.AddDays(1);

        return await _orderDbContextcontext.Orders
            .Where(o => o.CreatedAt >= start && o.CreatedAt < end)
            .Include(o => o.Items)
            .OrderBy(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetTodayOrderCountAsync(DateTime date)
    {
        var start = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        var end = start.AddDays(1);

        return await _orderDbContextcontext.Orders
            .CountAsync(o => o.CreatedAt >= start && o.CreatedAt < end);    
    }
}