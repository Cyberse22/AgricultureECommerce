using Microsoft.EntityFrameworkCore;
using OrderService.Data;

namespace OrderService.Repositories.Impl;

public class OrderRepositoryImpl : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepositoryImpl(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return new List<Order>();
        }

        return await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == userId)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(string orderId)
    {
        if (string.IsNullOrEmpty(orderId))
        {
            return null!;
        }

        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<bool> CancelOrderAsync(string orderId)
    {
        if (string.IsNullOrEmpty(orderId))
        {
            return false;
        }

        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null || order.Status == "Cancelled")
        {
            return false;
        }

        order.Status = "Cancelled";
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task CreateOrderAsync(Order order)
    {
        if (order == null || string.IsNullOrEmpty(order.OrderId))
        {
            throw new ArgumentException("Invalid order");
        }

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetTodayOrderCountAsync(DateTime today)
    {
        return await _context.Orders
            .CountAsync(o => o.CreatedAt.Date == today);
    }
}