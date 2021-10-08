using AcademyG.Week5.Test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week5.Test.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasMany(c => c.Expenses)
                .WithOne(e => e.Category);
        }
    }
}
