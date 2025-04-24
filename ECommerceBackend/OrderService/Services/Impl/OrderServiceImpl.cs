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


    public async Task<IEnumerable<OrderResponseModel>> GetAllOrdersAsync()
    {
        var order = await _orderRepository.GetAllOrdersAsync();
        return _mapper.Map<IEnumerable<OrderResponseModel>>(order);
    }

    public async Task<OrderResponseModel> GetOrderIdAsync(string orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        return _mapper.Map<OrderResponseModel>(order);
    }

    public async Task<OrderResponseModel> CreateOrderAsync(CreateOrderModel model)
    {
        var order = _mapper.Map<Order>(model);
        order.OrderId = $"OD{DateTime.UtcNow:yyyyMMddHHmmssfff}";

        if (order.PaymentMethod == "Cash")
        {
            order.Status = "Paid";
        }
        else
        {
            order.Status = "Pending";
        }
        
        var today = DateTime.UtcNow.Date;
        int todayCount = await _orderRepository.GetTodayOrderCountAsync(today);
        order.Index = todayCount + 1;
        
        order.TotalAmount = model.OrderItems.Sum(i =>
        {
            var subtotal = (i.UnitPrice ?? 0) * i.Quantity;
            var discount = subtotal * (decimal)(i.Discount ?? 0) / 100;
            return subtotal - discount;
        });
        
        order.Items = model.OrderItems.Select(i =>
        {
            var item = _mapper.Map<OrderItem>(i);
            item.OrderCode = order.OrderId;
            item.Subtotal = (i.UnitPrice ?? 0) * i.Quantity * (1 - (long)(i.Discount ?? 0) / 100);
            return item;
        }).ToList();
        
        var result = await _orderRepository.CreateOrderAsync(order);
        return _mapper.Map<OrderResponseModel>(result);
    }

    public async Task<IEnumerable<OrderResponseModel>> GetAllOrdersByDateAsync(DateTime date)
    {
        var orders = await _orderRepository.GetOrdersByDateAsync(date);
        return _mapper.Map<IEnumerable<OrderResponseModel>>(orders);
    }

    public async Task<IEnumerable<OrderWithIndexModel>> GetOrdersByDateWithIndex(DateTime date)
    {
        var orders = await _orderRepository.GetOrdersByDateAsync(date);
        int index = 1;
        return orders.Select(o => new OrderWithIndexModel
        {
            Index = index++,
            OrderId = o.OrderId,
            CustomerName = o.CustomerName,
            CustomerPhone = o.CustomerPhone,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            PaymentMethod = o.PaymentMethod
            
        }).ToList();
    }
}