using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaleFashion.Web.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    OldPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Colors = table.Column<string>(type: "TEXT", nullable: false),
                    Sizes = table.Column<string>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Category", "Colors", "Description", "ImageUrl", "Label", "Name", "OldPrice", "Price", "Rating", "Sizes", "Tags" },
                values: new object[,]
                {
                    { 1, "Gucci", "Jackets", "[\"Black\",\"Brown\"]", "Premium biker jacket with a quilted lining and adjustable hood.", "/img/product/luxury-jacket.png", "New", "Piqué Biker Jacket", null, 5500m, 4, "[\"M\",\"L\",\"XL\"]", "[\"Product\",\"Clothing\"]" },
                    { 2, "Chanel", "Jackets", "[\"Blue\"]", "A sharp formal suit jacket for men, crafted from premium fabric.", "/img/product/formal-suit.png", "", "Formal Navy Blazer", null, 4200m, 5, "[\"S\",\"M\",\"L\"]", "[\"Product\",\"Clothing\"]" },
                    { 3, "Hermes", "Shoes", "[\"Blue\",\"White\"]", "Modern low-top sneakers with a comfortable fit.", "/img/product/product-1.jpg", "Sale", "Low-top Sneakers", null, 1800m, 4, "[\"8\",\"9\",\"10\"]", "[\"Shoes\"]" },
                    { 4, "Louis Vuitton", "Accessories", "[\"Grey\"]", "Durable canvas duffel bag for gym or weekend trips.", "/img/product/product-11.jpg", "", "Canvas Duffel Bag", null, 2800m, 5, "[\"One Size\"]", "[\"Bags\",\"Accessories\"]" },
                    { 5, "Gucci", "Accessories", "[\"Black\"]", "Sophisticated leather backpack designed for work and travel.", "/img/product/product-5.jpg", "", "Leather Backpack", null, 3500m, 5, "[\"One Size\"]", "[\"Bags\",\"Accessories\"]" },
                    { 6, "Chanel", "Shoes", "[\"Black\"]", "Elegant ankle boots made from genuine leather.", "/img/product/product-6.jpg", "Sale", "Classic Ankle Boots", null, 6500m, 4, "[\"8\",\"9\",\"10\"]", "[\"Shoes\"]" },
                    { 7, "Hermes", "Clothing", "[\"White\",\"Blue\"]", "Casual T-shirt with a unique contrast pocket detail.", "/img/product/product-7.jpg", "", "T-shirt Contrast Pocket", null, 1200m, 5, "[\"S\",\"M\",\"L\"]", "[\"Clothing\"]" },
                    { 8, "Louis Vuitton", "Clothing", "[\"Multi\"]", "Lightweight flowing scarf that adds a touch of elegance.", "/img/product/product-8.jpg", "", "Basic Flowing Scarf", null, 850m, 5, "[\"One Size\"]", "[\"Accessories\"]" },
                    { 9, "Gucci", "Clothing", "[\"Blue\"]", "Classic vintage style blue shirt.", "/img/product/product-9.jpg", "New", "Vintage Blue Shirt", null, 2200m, 4, "[\"M\",\"L\",\"XL\"]", "[\"Clothing\"]" },
                    { 10, "Chanel", "Clothing", "[\"White\"]", "Sharp slim-fit shirt in breathable cotton.", "/img/product/product-10.jpg", "", "Slim Fit Cotton Shirt", null, 1950m, 5, "[\"S\",\"M\",\"L\"]", "[\"Clothing\"]" },
                    { 11, "Hermes", "Clothing", "[\"Grey\"]", "Comfortable casual hoodie with a minimalist design.", "/img/product/casual-hoodie.png", "Sale", "Casual Grey Hoodie", null, 2500m, 4, "[\"S\",\"M\",\"L\",\"XL\"]", "[\"Clothing\"]" },
                    { 12, "Louis Vuitton", "Clothing", "[\"Grey\"]", "Luxurious knit sweater made from fine wool blend.", "/img/product/product-12.jpg", "", "Premium Knit Sweater", null, 3800m, 5, "[\"M\",\"L\"]", "[\"Clothing\"]" },
                    { 13, "Gucci", "Clothing", "[\"Green\"]", "A stylish summer polo shirt in a pastel color.", "/img/product/summer-polo.png", "", "Classic Polo Shirt", null, 2800m, 4, "[\"M\",\"L\",\"XL\"]", "[\"Clothing\"]" },
                    { 14, "Chanel", "Accessories", "[\"Black\"]", "Sleek designer sunglasses with a macro shot perspective.", "/img/product/designer-sunglasses.png", "", "Aviator Sunglasses", null, 9000m, 5, "[\"One Size\"]", "[\"Accessories\"]" },
                    { 15, "Hermes", "Accessories", "[\"Brown\"]", "A luxury men's wristwatch with a leather strap.", "/img/product/premium-watch.png", "", "Luxury Wristwatch", null, 15000m, 5, "[\"One Size\"]", "[\"Accessories\"]" },
                    { 16, "Louis Vuitton", "Accessories", "[\"Black\"]", "Stylish textured cap with a custom fit.", "/img/product/product-4.jpg", "", "Diagonal Textured Cap", null, 750m, 5, "[\"One Size\"]", "[\"Hats\",\"Accessories\"]" },
                    { 17, "Gucci", "Accessories", "[\"Black\"]", "Practical urban backpack for daily use.", "/img/product/product-2.jpg", "", "Urban Utility Backpack", null, 3200m, 4, "[\"One Size\"]", "[\"Bags\"]" },
                    { 18, "Chanel", "Accessories", "[\"Black\"]", "Versatile chest bag with multiple compartments.", "/img/product/product-3.jpg", "", "Multi-pocket Chest Bag", null, 1500m, 5, "[\"One Size\"]", "[\"Bags\"]" },
                    { 19, "Hermes", "Accessories", "[\"Brown\"]", "High-quality leather belt with a timeless design.", "/img/product/product-13.jpg", "", "Premium Leather Belt", null, 4500m, 5, "[\"M\",\"L\"]", "[\"Accessories\"]" },
                    { 20, "Louis Vuitton", "Accessories", "[\"Black\"]", "Sleek designer wallet for minimalists.", "/img/product/product-14.jpg", "", "Designer Slim Wallet", null, 6000m, 5, "[\"One Size\"]", "[\"Accessories\"]" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "IsAdmin", "Password", "PhoneNumber", "Username" },
                values: new object[] { "admin-1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@smashcollections.com", true, true, "admin", "0000000000", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
