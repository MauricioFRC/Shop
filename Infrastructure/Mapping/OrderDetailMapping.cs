using Core.DTOs.OrderDetail;
using Core.Entities;
using Core.Request;
using Mapster;

namespace Infrastructure.Mapping;

public class OrderDetailMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderDetail, OrderDetailResponseDTO>()
            .Map(dest => dest.OrderId, src => src.OrderId)
            .Map(dest => dest.OrderDetailId, src => src.Id)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.UserName, src => src.Order.User.Name);

        config.NewConfig<CreateOrderDetailRequest, OrderDetail>()
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.OrderId, src => src.OrderId);

        config.NewConfig<UpdateOrderDetailDTO, OrderDetail>()
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.OrderId, src => src.OrderId);
    }
}
