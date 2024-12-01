﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.HasKey(c => c.Id);

        entity.Property(c => c.Name)
            .IsRequired();

        entity.HasMany(c => c.Products)
              .WithOne(c => c.Category)
              .HasForeignKey(c => c.CategoryId);
    }
}