using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Status)
            .IsRequired();

        entity.Property(x => x.Subject)
            .IsRequired();

        entity.Property(x => x.CreateAt)
            .IsRequired();

        entity.Property(x => x.Description)
            .IsRequired();

        entity.HasOne(x => x.User)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.UserId);
    }
}
