﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.ProductName)
            .IsRequired();

        entity.Property(x => x.Price)
            .IsRequired();

        entity.HasOne(x => x.Category)
              .WithMany(x => x.Products)
              .HasForeignKey(x => x.CategoryId);

        entity.HasMany(x => x.ProductImages)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}
