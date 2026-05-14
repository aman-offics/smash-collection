using System.Collections.Generic;
using System.Linq;
using MaleFashion.Web.Models;

namespace MaleFashion.Web.Services
{
    public class ProductRepository
    {
        private readonly Data.AppDbContext _context;

        public ProductRepository(Data.AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll() => _context.Products.ToList();

        public Product? GetById(int id) => _context.Products.FirstOrDefault(p => p.Id == id);

        public (IEnumerable<Product> Products, int TotalCount) GetProducts(string? search = null, string? category = null, string? brand = null, string? size = null, string? color = null, string? tag = null, decimal? minPrice = null, decimal? maxPrice = null, string? sortOrder = null, int page = 1, int pageSize = 9)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.ToLower() == category.ToLower());
            }

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(p => p.Brand.ToLower() == brand.ToLower());
            }

            if (!string.IsNullOrEmpty(size))
            {
                query = query.Where(p => p.Sizes.Any(s => s.ToLower() == size.ToLower()));
            }

            if (!string.IsNullOrEmpty(color))
            {
                query = query.Where(p => p.Colors.Any(c => c.ToLower() == color.ToLower()));
            }

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(p => p.Tags.Any(t => t.ToLower() == tag.ToLower()));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            switch (sortOrder)
            {
                case "PriceLowToHigh":
                    query = query.OrderBy(p => (double)p.Price);
                    break;
                case "PriceHighToLow":
                    query = query.OrderByDescending(p => (double)p.Price);
                    break;
                default:
                    break;
            }

            int totalCount = query.Count();
            var products = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return (products, totalCount);
        }
    }
}
