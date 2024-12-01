using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.HasKey(o => o.Id);

        entity.Property(o => o.TotalAmount)
              .IsRequired();

        entity.HasOne(o => o.Payment)
              .WithOne(o => o.Order)
              .HasForeignKey<Payment>(o => o.OrderId);

        entity.HasOne(o => o.User)
              .WithMany(o => o.Orders)
              .HasForeignKey(o => o.UserId);

        entity.HasMany(o => o.OrderDetails)
              .WithOne(o => o.Order)
              .HasForeignKey(o => o.OrderId);
    }
}
