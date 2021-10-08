using AcademyG.Week5.Test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week5.Test.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder
                .Property(e => e.Date)
                .IsRequired();

            builder
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(e => e.User)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(e => e.Amount)
                .IsRequired()
                .HasPrecision(10, 2);

            builder
                .Property(e => e.Approved)
                .IsRequired();

            builder
                .HasOne(e => e.Category)
                .WithMany(c => c.Expenses);
        }
    }
}
