using AcademyG.Week5.Test.Configurations;
using AcademyG.Week5.Test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AcademyG.Week5.Test.Context
{
    public class ExpensesContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ExpensesContext() : base() { }

        public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                                                     .SetBasePath(Directory.GetCurrentDirectory())
                                                     .AddJsonFile("appsettings.json")
                                                     .Build();

                string connStringSQL = config.GetConnectionString("TestWeek5");

                optionsBuilder.UseSqlServer(connStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Type).IsUnique(); //vincolo di unique sul tipo della categoria
        }
    }
}
