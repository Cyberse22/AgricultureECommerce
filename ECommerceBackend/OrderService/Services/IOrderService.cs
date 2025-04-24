using OrderService.Models;
using OrderService.Helpers;

namespace OrderService.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetAllOrdersAsync();
    Task<OrderResponseModel> GetOrderIdAsync(string orderId); 
    Task<OrderResponseModel> CreateOrderAsync(CreateOrderModel model);
    Task <IEnumerable<OrderResponseModel>> GetAllOrdersByDateAsync(DateTime date);
    Task <IEnumerable<OrderWithIndexModel>> GetOrdersByDateWithIndex(DateTime date);
}