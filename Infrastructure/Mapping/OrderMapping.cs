using Core.DTOs.Order;
using Core.Entities;
using Core.Request;
using Mapster;

namespace Infrastructure.Mapping;

public class OrderMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderResponseDTO>()
            .Map(dest => dest.OrderId, src => src.Id)
            .Map(dest => dest.OrderDate, src => src.OrderDate.ToShortDateString())
            .Map(dest => dest.TotalAmount, src => src.TotalAmount)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.UserId, src => src.UserId);

        config.NewConfig<CreateOrderRequest, Order>()
            .Map(dest => dest.OrderDate, src => src.OrderDate)
            .Map(dest => dest.TotalAmount, src => src.TotalAmount)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.UserId, src => src.UserId);

        config.NewConfig<UpdateOrderDTO, Order>()
            .Map(dest => dest.OrderDate, src => src.OrderDate)
            .Map(dest => dest.TotalAmount, src => src.TotalAmount)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.UserId, src => src.UserId);
    }
}
