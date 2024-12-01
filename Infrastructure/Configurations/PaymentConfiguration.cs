using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {
        entity.HasKey(p => p.Id);

        entity.Property(p => p.Amount)
              .IsRequired();

        entity.Property(p => p.PaymentMethod)
              .IsRequired();

        entity.HasOne(p => p.Order)
              .WithOne(p => p.Payment)
              .HasForeignKey<Payment>(p => p.OrderId);
    }
}
