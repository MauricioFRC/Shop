using Core.DTOs.Payment;
using Core.Entities;
using Core.Request;
using Mapster;

namespace Infrastructure.Mapping;

public class PaymentMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Payment, PaymentResponseDTO>()
            .Map(dest => dest.OrderId, src => src.OrderId)
            .Map(dest => dest.UserId, src => src.Order.User.Id)
            .Map(dest => dest.User, src => src.Order.User.Name)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.PaymentMethod, src => src.PaymentMethod)
            .Map(dest => dest.PaymentDate, src => src.PaymentDate.ToShortDateString());

        config.NewConfig<CreatePaymentRequest, Payment>()
            .Map(dest => dest.OrderId, src => src.OrderId)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.PaymentDate, src => DateTime.UtcNow)
            .Map(dest => dest.PaymentMethod, src => src.PaymentMethod);

        config.NewConfig<UpdatePaymentDTO, Payment>()
            .Map(dest => dest.OrderId, src => src.OrderId)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.PaymentDate, src => DateTime.UtcNow)
            .Map(dest => dest.PaymentMethod, src => src.PaymentMethod);
    }
}
