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

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string sortOrder, string searchString)
        {
            var products = _context.Products
                    .Include(p => p.Category)
                    .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                var loweredSearch = searchString.ToLower();

                products = products.Where(p =>
                    p.ProductName.ToLower().Contains(loweredSearch) ||
                    p.Category.CategoryName.ToLower().Contains(loweredSearch));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(product => product.ProductName);
                    break;
                case "Date":
                    products = products.OrderBy(product => product.DateAdded);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(product => product.DateAdded);
                    break;
                default:
                    products = products.OrderBy(product => Guid.NewGuid());
                    break;
            }

            return await products.AsNoTracking().ToListAsync();
        }
    }
}
