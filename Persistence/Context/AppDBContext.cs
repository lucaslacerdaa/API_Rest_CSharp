using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_rest.Domain.Models;
using api_rest.Domain.Helpers;


namespace api_rest.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

            builder.Entity<Category>().HasData
            (
                new Category { Id = 100, Name = "J. K. Rowling" }, // Id set manually due to in-memory provider
                new Category { Id = 101, Name = "Stephen Hawking" },
                new Category { Id = 99, Name = "John Zeratsky " }
            );

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(p => p.QuantityInPackage).IsRequired();
            builder.Entity<Product>().Property(p => p.UnitOfMeasurement).IsRequired();

            builder.Entity<Product>().HasData
            (
                new Product
                {
                    Id = 100,
                    Name = "Harry Potter e o Prisioneiro de Azkaban",
                    QuantityInPackage = 1,
                    UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    CategoryId = 100,
                },
                new Product
                {
                    Id = 101,
                    Name = "Breves respostas para grandes questões",
                    QuantityInPackage = 2,
                    UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    CategoryId = 101,
                },
                new Product
                {
                    Id = 99,
                    Name = "Sprint. Criação em cinco dias",
                    QuantityInPackage = 2,
                    UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    CategoryId = 99,
                },
                new Product
                {
                    Id = 103,
                    Name = "Uma Breve História do Tempo",
                    QuantityInPackage = 2,
                    UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    CategoryId = 101,
                }
            );


        }
    
    }
}
