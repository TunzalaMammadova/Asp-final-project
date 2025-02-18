﻿using System;
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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Adventage>()
                       .HasData(
            new Adventage
            {
                Id = 1,
                Icon = "fas fa-car-side fa-3x text-white",
                Title = "Free Shipping",
                Desc = "Free on order over $300"
            },

            new Adventage
            {
                Id = 2,
                Icon = "fas fa-user-shield fa-3x text-white",
                Title = "Security Payment",
                Desc = "100% security payment"
            },

            new Adventage
            {
                Id = 3,
                Icon = "fas fa-exchange-alt fa-3x text-white",
                Title = "30 Day Return",
                Desc = "30 day money guarantee"
            },

            new Adventage
            {
                Id = 4,
                Icon = "fa fa-phone-alt fa-3x text-white",
                Title = "24/7 Support",
                Desc = "Support every time fast"
            }
          );


            modelBuilder.Entity<Customer>()
                        .HasData(
            new Customer
            {
                Id = 1,
                Icon = "fa fa-users text-secondary",
                Title = "SATISFIED CUSTOMERS",
                Desc = "1963"
            },

            new Customer
            {
                Id = 2,
                Icon = "fa fa-users text-secondary",
                Title = "QUALITY OF SERVICE",
                Desc = "99%"
            },

            new Customer
            {
                Id = 3,
                Icon = "fa fa-users text-secondary",
                Title = "QUALITY CERTIFICATES",
                Desc = "33"
            },

            new Customer
            {
                Id = 4,
                Icon = "fa fa-users text-secondary",
                Title = "AVAILABLE PRODUCTS",
                Desc = "789"
            }
          );


            modelBuilder.Entity<Marketing>()
                        .HasData(
            new Marketing
            {
                Id = 1,
                Title = "Fresh Exotic Fruits",
                Description = "The generated Lorem Ipsum is therefore always free from repetition injected humour, or non-characteristic words etc.",
                Image = "baner-1.png"
            }
           );


            modelBuilder.Entity<Sale>()
                       .HasData(
            new Sale
            {
                Id = 1,
                Title = "Fresh Apples",
                Description = "20% OFF",
                Image = "featur-1.jpg"
            },

            new Sale
            {
                Id = 2,
                Title = "Tasty Fruits",
                Description = "Free delivery",
                Image = "featur-2.jpg"
            },

            new Sale
            {
                Id = 3,
                Title = "Exotic Vegitable",
                Description = "Discount 30$",
                Image = "featur-3.jpg"
            }
           );


            modelBuilder.Entity<Setting>()
                       .HasData(
            new Setting
            {
                Id = 1,
                LogoName = "FruiTables",
                Address = "123 Street, New York",
                Email = "Email@Example.com"
            }
           );


            modelBuilder.Entity<Slider>()
                       .HasData(
            new Slider
            {
                Id = 1,
                Title = "Fruits",
                Image = "hero-img-1.png"
            },

            new Slider
            {
                Id = 2,
                Title = "Vegitables",
                Image = "hero-img-2.png"
            }
           );


            modelBuilder.Entity<SliderInfo>()
                        .HasData(
            new SliderInfo
            {
                Id = 1,
                Title = "100% Organic Foods",
                Description = "Organic Veggies & Fruits Foods",
                Background = "hero-img.jpg"
            }
           );


            modelBuilder.Entity<Category>()
                        .HasData(
            new Category
            {
                Id = 1,
                Name = "Vegetables"
            },

            new Category
            {
                Id = 2,
                Name = "Fruits"
            },

            new Category
            {
                Id = 3,
                Name = "Bread"
            },

            new Category
            {
                Id = 4,
                Name = "Meat"
            }
           );

            modelBuilder.Entity<Product>()
                        .HasData(
            new Product
            {
                Id = 1,
                Name = "Grapes",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 30,
                CategoryId = 3
            },

            new Product
            {
                Id = 2,
                Name = "Grapes",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 50,
                CategoryId = 3
            },

            new Product
            {
                Id = 3,
                Name = "Raspberries",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 20,
                CategoryId = 2
            },

            new Product
            {
                Id = 4,
                Name = "Apricots",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 10,
                CategoryId = 1
            },

            new Product
            {
                Id = 5,
                Name = "Banana",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 15,
                CategoryId = 4
            },

            new Product
            {
                Id = 6,
                Name = "Oranges",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 25,
                CategoryId = 2
            },

            new Product
            {
                Id = 7,
                Name = "Raspberries",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 35,
                CategoryId = 1
            },

            new Product
            {
                Id = 8,
                Name = "Banana",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 40,
                CategoryId = 4
            },

            new Product
            {
                Id = 9,
                Name = "0ranges",
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod te incididunt",
                Price = 45,
                CategoryId = 3
            }
           );


            modelBuilder.Entity<ProductImage>()
                        .HasData(
            new ProductImage
            {
                Id = 1,
                Name = "fruite-item-5.jpg",
                ProductId = 1
            },

            new ProductImage
            {
                Id = 2,
                Name = "fruite-item-2.jpg",
                ProductId = 2
            },

            new ProductImage
            {
                Id = 3,
                Name = "fruite-item-3.jpg",
                ProductId = 3
            },

            new ProductImage
            {
                Id = 4,
                Name = "fruite-item-4.jpg",
                ProductId = 4
            },

            new ProductImage
            {
                Id = 5,
                Name = "fruite-item-4.jpg",
                ProductId = 5
            },

            new ProductImage
            {
                Id = 6,
                Name = "fruite-item-3.jpg",
                ProductId = 6
            },

            new ProductImage
            {
                Id = 7,
                Name = "fruite-item-5.jpg",
                ProductId = 7
            },

            new ProductImage
            {
                Id = 8,
                Name = "fruite-item-2.jpg",
                ProductId = 8
            },

            new ProductImage
            {
                Id = 9,
                Name = "fruite-item-4.jpg",
                ProductId = 9
            }
            );
        }
    }
}
