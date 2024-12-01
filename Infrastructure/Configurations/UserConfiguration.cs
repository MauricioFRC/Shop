using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);

        entity.Property(u => u.Name)
              .IsRequired();

        entity.Property(u => u.Email)
              .IsRequired();

        entity.Property(u => u.Password)
              .IsRequired();

        entity.HasMany(u => u.Orders)
              .WithOne(u => u.User)
              .HasForeignKey(u => u.UserId);
    }
}
