using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> entity)
    {
        entity.HasKey(od => od.OrderId);

        entity.Property(od => od.Quantity)
              .IsRequired();

        entity.Property(od => od.Price)
              .IsRequired();

        entity.HasOne(od => od.Order)
              .WithMany(od => od.OrderDetails)
              .HasForeignKey(od => od.OrderId);
    }
}
