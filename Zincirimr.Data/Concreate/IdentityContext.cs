using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Concreate
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasMany(p => p.Categories).WithMany(c => c.Products)
                .UsingEntity<ProductCategory>();
            builder.Entity<Category>().HasIndex(u => u.Url).IsUnique();
            builder.Entity<Product>().HasIndex(p => p.Url).IsUnique();

            builder.Entity<Product>().HasData(new List<Product>()
            {
                new Product
                {
                    Id = 1, Image = "iphone1.png", Name = "iPhone 15 Plus", Price = 75000, Url = "iphone15plus"
                },
                new Product
                {
                    Id = 2, Image = "iphone1.png", Name = "iPhone 15 Pro Max", Price = 95000, Url = "iphone15promax"
                },
                new Product
                {
                    Id = 3, Image = "iphone1.png", Name = "iPhone 15 Pro", Price = 95000, Url = "iphone15pro"
                },
                new Product
                {
                    Id = 4, Image = "iphone1.png", Name = "iPhone 15", Price = 95000, Url = "iphone15"
                },
                new Product
                {
                    Id = 5, Image = "iphone1.png", Name = "iPhone 14 Plus", Price = 95000, Url = "iphone14plus"
                },
                new Product
                {
                    Id = 6, Image = "iphone1.png", Name = "iPhone 14 Pro Max", Price = 95000, Url = "iphone14promax"
                },
                new Product
                {
                    Id = 7, Image = "iphone1.png", Name = "iPhone 14 Pro", Price = 95000, Url = "iphone14pro"
                },
                new Product
                {
                    Id = 8, Image = "iphone1.png", Name = "iPhone 14", Price = 95000, Url = "iphone14"
                },
                new Product
                {
                    Id = 9, Image = "iphone1.png", Name = "iPhone 13 Pro Max", Price = 95000, Url = "iphone13promax"
                },
                new Product
                {
                    Id = 10, Image = "macbook.png", Name = "MacBook Air M2", Price = 95000, Url = "macbookairm2"
                },
            });
            builder.Entity<Category>().HasData(new List<Category>
            {
                new Category { Id = 1, Name = "Telefon", Url = "telefon" },
                new Category { Id = 2, Name = "Tablet", Url = "tablet" },
                new Category { Id = 3, Name = "Bilgisayar", Url = "bilgisayar" },
                new Category { Id = 4, Name = "Laptop", Url = "laptop" },
                new Category { Id = 6, Name = "Eletronik", Url = "Elektronik" },
                new Category { Id = 5, Name = "Akıllı Ev Aletleri", Url = "akillievaletleri" },
            });

            builder.Entity<ProductCategory>().HasData(new List<ProductCategory>
            {
                new ProductCategory { ProductId = 1, CategoryId = 1 },
                new ProductCategory { ProductId = 2, CategoryId = 1 },
                new ProductCategory { ProductId = 3, CategoryId = 1 },
                new ProductCategory { ProductId = 4, CategoryId = 1 },
                new ProductCategory { ProductId = 5, CategoryId = 1 },
                new ProductCategory { ProductId = 6, CategoryId = 1 },
                new ProductCategory { ProductId = 7, CategoryId = 1 },
                new ProductCategory { ProductId = 8, CategoryId = 1 },
                new ProductCategory { ProductId = 9, CategoryId = 1 },
                new ProductCategory { ProductId = 10, CategoryId = 3 },
            });

        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

    }
}
