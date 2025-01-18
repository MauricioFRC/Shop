using Core.DTOs.Ticket;
using Core.Entities;
using Core.Request;
using Mapster;

namespace Infrastructure.Mapping;

public class TicketMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTicketRequest, Ticket>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Subject, src => src.Subject)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Priority, src => src.Priority)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.CreateAt, src => src.CreateAt);

        config.NewConfig<Ticket, TicketResponseDto>()
            .Map(dest => dest.TicketId, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.UserName, src => src.User.Name)
            .Map(dest => dest.Subject, src => src.Subject)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Priority, src => src.Priority)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.CreateAt, src => src.CreateAt.ToShortDateString());
    }
}
