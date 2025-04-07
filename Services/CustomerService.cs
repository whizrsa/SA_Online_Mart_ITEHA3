using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppUser>> GetAllCustomersAsync()
        {
            var customers = await _context.Users.ToListAsync();

            return customers;
        }
    }
}
