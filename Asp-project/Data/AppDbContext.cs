using System;
using Asp_project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Adventage> Advantages { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Marketing> Marketings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Adventage>()
    //                   .HasData(
    //        new Adventage
    //        {
    //            Id = 1,
    //            Icon = "fas fa-car-side fa-3x text-white",
    //            Title = "Free Shipping",
    //            Desc = "Free on order over $300"
    //        },

    //        new Adventage
    //        {
    //            Id = 2,
    //            Icon = "fas fa-user-shield fa-3x text-white",
    //            Title = "Security Payment",
    //            Desc = "100% security payment"
    //        },

    //        new Adventage
    //        {
    //            Id = 3,
    //            Icon = "fas fa-exchange-alt fa-3x text-white",
    //            Title = "30 Day Return",
    //            Desc = "30 day money guarantee"
    //        },

    //        new Adventage
    //        {
    //            Id = 4,
    //            Icon = "fa fa-phone-alt fa-3x text-white",
    //            Title = "24/7 Support",
    //            Desc = "Support every time fast"
    //        }
    //      );


    //        modelBuilder.Entity<Customer>()
    //                   .HasData(
    //        new Customer
    //        {
    //            Id = 1,
    //            Icon = "fa fa-users text-secondary",
    //            Title = "SATISFIED CUSTOMERS",
    //            Desc = "1963"
    //        },

    //        new Customer
    //        {
    //            Id = 2,
    //            Icon = "fa fa-users text-secondary",
    //            Title = "QUALITY OF SERVICE",
    //            Desc = "99%"
    //        },

    //        new Customer
    //        {
    //            Id = 3,
    //            Icon = "fa fa-users text-secondary",
    //            Title = "QUALITY CERTIFICATES",
    //            Desc = "33"
    //        },

    //        new Customer
    //        {
    //            Id = 4,
    //            Icon = "fa fa-users text-secondary",
    //            Title = "AVAILABLE PRODUCTS",
    //            Desc = "789"
    //        }
    //      );


    //        modelBuilder.Entity<Marketing>()
    //                   .HasData(
    //        new Marketing
    //        {
    //            Id = 1,
    //            Title = "Fresh Exotic Fruits",
    //            Description = "The generated Lorem Ipsum is therefore always free from repetition injected humour, or non-characteristic words etc.",
    //            Image = "baner-1.png"
    //        }
    //       );


    //        modelBuilder.Entity<Sale>()
    //                   .HasData(
    //        new Sale
    //        {
    //            Id = 1,
    //            Title = "Fresh Apples",
    //            Description = "20% OFF",
    //            Image = "featur-1.jpg"
    //        },

    //        new Sale
    //        {
    //            Id = 2,
    //            Title = "Tasty Fruits",
    //            Description = "Free delivery",
    //            Image = "featur-2.jpg"
    //        },

    //        new Sale
    //        {
    //            Id = 3,
    //            Title = "Exotic Vegitable",
    //            Description = "Discount 30$",
    //            Image = "featur-3.jpg"
    //        }
    //       );


    //        modelBuilder.Entity<Setting>()
    //                   .HasData(
    //        new Setting
    //        {
    //            Id = 1,
    //            LogoName = "FruiTables",
    //            Address = "123 Street, New York",
    //            Email = "Email@Example.com"
    //        }
    //       );


    //       modelBuilder.Entity<Slider>()
    //                  .HasData(
    //       new Slider
    //       {
    //           Id = 1,
    //           Title = "Fruits",
    //           Image = "hero-img-1.png"
    //       },

    //       new Slider
    //       {
    //           Id = 2,
    //           Title = "Vegitables",
    //           Image = "hero-img-2.png"
    //       }
    //      );


    //       modelBuilder.Entity<SliderInfo>()
    //                  .HasData(
    //       new SliderInfo
    //       {
    //           Id = 1,
    //           Title = "100% Organic Foods",
    //           Description = "Organic Veggies & Fruits Foods",
    //           Background = "hero-img.jpg"
    //       }
    //      );


    //       modelBuilder.Entity<Category>()
    //                   .HasData(
    //       new Category
    //       {
    //           Id = 1,
    //           Name = "Vegetables"
    //       },

    //       new Category
    //       {
    //           Id = 2,
    //           Name = "Fruits"
    //       },

    //       new Category
    //       {
    //           Id = 3,
    //           Name = "Bread"
    //       },

    //       new Category
    //       {
    //           Id = 4,
    //           Name = "Meat"
    //       }
    //      );

    //    }
    //}
}