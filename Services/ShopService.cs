using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public class ShopService : IShopService
    {
        private readonly ApplicationDbContext _context;
        public ShopService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return products;
        }
    }
}
