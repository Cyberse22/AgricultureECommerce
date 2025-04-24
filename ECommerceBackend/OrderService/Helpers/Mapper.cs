using AutoMapper;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Helpers;

public class Mapper : Profile
{
    public Mapper()
    {
        // Mapping cho OrderItemModel <-> OrderItem
        CreateMap<OrderItemModel, OrderItem>()
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src =>
                (src.UnitPrice ?? 0) * src.Quantity * (1 - (double)(src.Discount ?? 0) / 100)));

        CreateMap<OrderItem, OrderItemModel>();

        // Mapping cho CreateOrderModel <-> Order
        CreateMap<CreateOrderModel, Order>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore()) // sẽ tự tính trong service
            .ForMember(dest => dest.Status, opt => opt.Ignore())      // set thủ công
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())     // set thủ công
            .ReverseMap();

        // Mapping cho Order <-> OrderResponseModel
        CreateMap<Order, OrderResponseModel>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<OrderResponseModel, Order>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}