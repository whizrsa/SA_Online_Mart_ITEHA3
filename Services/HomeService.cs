using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _context;
        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return product;

        }
        public async Task<IEnumerable<Product>> SortProducts()
        {
            var latestProducts = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => Guid.NewGuid())
                .Take(6)
                .ToListAsync();

            return latestProducts;
        }
    }
}
