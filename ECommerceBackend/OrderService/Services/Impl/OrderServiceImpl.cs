using AutoMapper;
using OrderService.Models;
using OrderService.Data;
using OrderService.Repositories;

namespace OrderService.Services;

public class OrderServiceImpl : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderServiceImpl(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderModel>> GetOrdersByUserIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentException("UserId is required");
        }

        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        return _mapper.Map<List<OrderModel>>(orders);
    }

    public async Task<OrderModel> GetOrderByIdAsync(string orderId)
    {
        if (string.IsNullOrEmpty(orderId))
        {
            throw new ArgumentException("OrderId is required");
        }

        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        return _mapper.Map<OrderModel>(order);
    }

    public async Task<bool> CancelOrderAsync(string orderId)
    {
        if (string.IsNullOrEmpty(orderId))
        {
            return false;
        }

        return await _orderRepository.CancelOrderAsync(orderId);
    }
}