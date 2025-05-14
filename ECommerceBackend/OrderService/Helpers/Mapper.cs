using AutoMapper;
using OrderService.Data;
using OrderService.Models;
using Shared.Messaging;

namespace OrderService.Helpers;

public class Mapper : Profile
{
    public Mapper()
    {
        // Map CartMessage to Order
        CreateMap<CartMessage, Order>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.Items, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Index, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // Map CartItemModel to OrderItem
        CreateMap<CartItemModel, OrderItem>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Price))
            //.ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount ?? 0))
            .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
            .ForMember(dest => dest.OrderCode, opt => opt.Ignore());

        // Map Order to OrderDto
        CreateMap<Order, OrderModel>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map OrderItem to OrderItemDto
        CreateMap<OrderItem, OrderItemModel>();
        CreateMap<CartMessage, Order>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone));

        CreateMap<CartItemModel, OrderItem>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Price));
    }
}