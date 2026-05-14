using Microsoft.EntityFrameworkCore;
using MaleFashion.Web.Models;
using System.Collections.Generic;

namespace MaleFashion.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Admin User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "admin-1",
                    Username = "admin",
                    Password = "admin",
                    Email = "admin@smashcollections.com",
                    PhoneNumber = "0000000000",
                    IsAdmin = true,
                    IsActive = true,
                    CreatedAt = new System.DateTime(2025, 1, 1, 0, 0, 0, System.DateTimeKind.Utc)
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Piqué Biker Jacket", Price = 5500, ImageUrl = "/img/product/luxury-jacket.png", Label = "New", Rating = 4, Brand = "Gucci", Category = "Jackets", Colors = new List<string>{"Black", "Brown"}, Sizes = new List<string>{"M", "L", "XL"}, Tags = new List<string>{"Product", "Clothing"}, Description = "Premium biker jacket with a quilted lining and adjustable hood." },
                new Product { Id = 2, Name = "Formal Navy Blazer", Price = 4200, ImageUrl = "/img/product/formal-suit.png", Rating = 5, Brand = "Chanel", Category = "Jackets", Colors = new List<string>{"Blue"}, Sizes = new List<string>{"S", "M", "L"}, Tags = new List<string>{"Product", "Clothing"}, Description = "A sharp formal suit jacket for men, crafted from premium fabric." },
                new Product { Id = 3, Name = "Low-top Sneakers", Price = 1800, ImageUrl = "/img/product/product-1.jpg", Label = "Sale", Rating = 4, Brand = "Hermes", Category = "Shoes", Colors = new List<string>{"Blue", "White"}, Sizes = new List<string>{"8", "9", "10"}, Tags = new List<string>{"Shoes"}, Description = "Modern low-top sneakers with a comfortable fit." },
                new Product { Id = 4, Name = "Canvas Duffel Bag", Price = 2800, ImageUrl = "/img/product/product-11.jpg", Rating = 5, Brand = "Louis Vuitton", Category = "Accessories", Colors = new List<string>{"Grey"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Bags", "Accessories"}, Description = "Durable canvas duffel bag for gym or weekend trips." },
                new Product { Id = 5, Name = "Leather Backpack", Price = 3500, ImageUrl = "/img/product/product-5.jpg", Rating = 5, Brand = "Gucci", Category = "Accessories", Colors = new List<string>{"Black"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Bags", "Accessories"}, Description = "Sophisticated leather backpack designed for work and travel." },
                new Product { Id = 6, Name = "Classic Ankle Boots", Price = 6500, ImageUrl = "/img/product/product-6.jpg", Label = "Sale", Rating = 4, Brand = "Chanel", Category = "Shoes", Colors = new List<string>{"Black"}, Sizes = new List<string>{"8", "9", "10"}, Tags = new List<string>{"Shoes"}, Description = "Elegant ankle boots made from genuine leather." },
                new Product { Id = 7, Name = "T-shirt Contrast Pocket", Price = 1200, ImageUrl = "/img/product/product-7.jpg", Rating = 5, Brand = "Hermes", Category = "Clothing", Colors = new List<string>{"White", "Blue"}, Sizes = new List<string>{"S", "M", "L"}, Tags = new List<string>{"Clothing"}, Description = "Casual T-shirt with a unique contrast pocket detail." },
                new Product { Id = 8, Name = "Basic Flowing Scarf", Price = 850, ImageUrl = "/img/product/product-8.jpg", Rating = 5, Brand = "Louis Vuitton", Category = "Clothing", Colors = new List<string>{"Multi"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Accessories"}, Description = "Lightweight flowing scarf that adds a touch of elegance." },
                new Product { Id = 9, Name = "Vintage Blue Shirt", Price = 2200, ImageUrl = "/img/product/product-9.jpg", Label = "New", Rating = 4, Brand = "Gucci", Category = "Clothing", Colors = new List<string>{"Blue"}, Sizes = new List<string>{"M", "L", "XL"}, Tags = new List<string>{"Clothing"}, Description = "Classic vintage style blue shirt." },
                new Product { Id = 10, Name = "Slim Fit Cotton Shirt", Price = 1950, ImageUrl = "/img/product/product-10.jpg", Rating = 5, Brand = "Chanel", Category = "Clothing", Colors = new List<string>{"White"}, Sizes = new List<string>{"S", "M", "L"}, Tags = new List<string>{"Clothing"}, Description = "Sharp slim-fit shirt in breathable cotton." },
                new Product { Id = 11, Name = "Casual Grey Hoodie", Price = 2500, ImageUrl = "/img/product/casual-hoodie.png", Label = "Sale", Rating = 4, Brand = "Hermes", Category = "Clothing", Colors = new List<string>{"Grey"}, Sizes = new List<string>{"S", "M", "L", "XL"}, Tags = new List<string>{"Clothing"}, Description = "Comfortable casual hoodie with a minimalist design." },
                new Product { Id = 12, Name = "Premium Knit Sweater", Price = 3800, ImageUrl = "/img/product/product-12.jpg", Rating = 5, Brand = "Louis Vuitton", Category = "Clothing", Colors = new List<string>{"Grey"}, Sizes = new List<string>{"M", "L"}, Tags = new List<string>{"Clothing"}, Description = "Luxurious knit sweater made from fine wool blend." },
                new Product { Id = 13, Name = "Classic Polo Shirt", Price = 2800, ImageUrl = "/img/product/summer-polo.png", Rating = 4, Brand = "Gucci", Category = "Clothing", Colors = new List<string>{"Green"}, Sizes = new List<string>{"M", "L", "XL"}, Tags = new List<string>{"Clothing"}, Description = "A stylish summer polo shirt in a pastel color." },
                new Product { Id = 14, Name = "Aviator Sunglasses", Price = 9000, ImageUrl = "/img/product/designer-sunglasses.png", Rating = 5, Brand = "Chanel", Category = "Accessories", Colors = new List<string>{"Black"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Accessories"}, Description = "Sleek designer sunglasses with a macro shot perspective." },
                new Product { Id = 15, Name = "Luxury Wristwatch", Price = 15000, ImageUrl = "/img/product/premium-watch.png", Rating = 5, Brand = "Hermes", Category = "Accessories", Colors = new List<string>{"Brown"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Accessories"}, Description = "A luxury men's wristwatch with a leather strap." },
                new Product { Id = 16, Name = "Diagonal Textured Cap", Price = 750, ImageUrl = "/img/product/product-4.jpg", Rating = 5, Brand = "Louis Vuitton", Category = "Accessories", Colors = new List<string>{"Black"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Hats", "Accessories"}, Description = "Stylish textured cap with a custom fit." },
                new Product { Id = 17, Name = "Urban Utility Backpack", Price = 3200, ImageUrl = "/img/product/product-2.jpg", Rating = 4, Brand = "Gucci", Category = "Accessories", Colors = new List<string>{"Black"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Bags"}, Description = "Practical urban backpack for daily use." },
                new Product { Id = 18, Name = "Multi-pocket Chest Bag", Price = 1500, ImageUrl = "/img/product/product-3.jpg", Rating = 5, Brand = "Chanel", Category = "Accessories", Colors = new List<string>{"Black"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Bags"}, Description = "Versatile chest bag with multiple compartments." },
                new Product { Id = 19, Name = "Premium Leather Belt", Price = 4500, ImageUrl = "/img/product/product-13.jpg", Rating = 5, Brand = "Hermes", Category = "Accessories", Colors = new List<string>{"Brown"}, Sizes = new List<string>{"M", "L"}, Tags = new List<string>{"Accessories"}, Description = "High-quality leather belt with a timeless design." },
                new Product { Id = 20, Name = "Designer Slim Wallet", Price = 6000, ImageUrl = "/img/product/product-14.jpg", Rating = 5, Brand = "Louis Vuitton", Category = "Accessories", Colors = new List<string>{"Black"}, Sizes = new List<string>{"One Size"}, Tags = new List<string>{"Accessories"}, Description = "Sleek designer wallet for minimalists." }
            );
        }
    }
}
