using AutoMapper;
using MassTransit;
using OrderService.Data;
using OrderService.Models;
using OrderService.Repositories;
using Shared.Messaging;

namespace OrderService.Services.Impl
{
    public class CartConsumer : IConsumer<CartMessage>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CartConsumer> _logger;

        public CartConsumer(IOrderRepository orderRepository, IMapper mapper, IPublishEndpoint publishEndpoint, ILogger<CartConsumer> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CartMessage> context)
        {
            var message = context.Message;

            //// Validate CartMessage
            //if (string.IsNullOrEmpty(message.UserId))
            //{
            //    Console.WriteLine("Invalid CartMessage: UserId is missing");
            //    await _publishEndpoint.Publish(new OrderCreationFailedMessage
            //    {
            //        UserId = "Unknown",
            //        ErrorMessage = "UserId is missing in CartMessage",
            //        Timestamp = DateTime.UtcNow
            //    });
            //    return;
            //}

            //if (!message.Items.Any())
            //{
            //    Console.WriteLine($"Invalid CartMessage: No items for UserId: {message.UserId}");
            //    await _publishEndpoint.Publish(new OrderCreationFailedMessage
            //    {
            //        UserId = message.UserId,
            //        ErrorMessage = "No items in CartMessage",
            //        Timestamp = DateTime.UtcNow
            //    });
            //    return;
            //}

            try
            {
                // Map CartMessage to Order
                var order = _mapper.Map<Order>(message);
                order.OrderId = $"OD{message.UserId}{DateTime.UtcNow:yyyyMMddHHmmss}";
                order.Status = message.PaymentMethod == "Cash" ? "Paid" : "Pending";
                order.CreatedAt = DateTime.UtcNow;

                // Set order index
                var today = DateTime.UtcNow.Date;
                int todayCount = await _orderRepository.GetTodayOrderCountAsync(today);
                order.Index = todayCount + 1;

                // Map items and calculate subtotals
                order.Items = message.Items.Select(i =>
                {
                    var item = _mapper.Map<OrderItem>(i);
                    item.OrderCode = order.OrderId;
                    item.Subtotal = (item.UnitPrice ?? 0) * item.Quantity * (1 - (item.Discount ?? 0) / 100);
                    return item;
                }).ToList();

                // Calculate TotalAmount
                order.TotalAmount = order.Items.Sum(i =>
                {
                    var subtotal = (i.UnitPrice ?? 0) * i.Quantity;
                    var discount = subtotal * (i.Discount ?? 0) / 100;
                    return subtotal - discount;
                });

                // Save order
                await _orderRepository.CreateOrderAsync(order);

                //// Publish OrderCreatedMessage
                //await _publishEndpoint.Publish(new OrderCreatedMessage
                //{
                //    OrderId = order.OrderId,
                //    UserId = order.CustomerId,
                //    Status = order.Status,
                //    TotalAmount = order.TotalAmount
                //});

                Console.WriteLine($"Order created: {order.OrderId} for UserId: {order.CustomerId}");
                _logger.LogInformation("🔥 Received CartMessage from RabbitMQ: {UserId}, Payment: {PaymentMethod}, Items: {Count}", message.UserId, message.PaymentMethod, message.Items.Count);

                // TODO: Tạo đơn hàng ở đây
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error processing CartMessage for UserId: {message.UserId}. Error: {ex.Message}");
                //await _publishEndpoint.Publish(new OrderCreationFailedMessage
                //{
                //    UserId = message.UserId,
                //    ErrorMessage = ex.Message,
                //    Timestamp = DateTime.UtcNow
                //});
                //throw;
            }
        }
    }
}