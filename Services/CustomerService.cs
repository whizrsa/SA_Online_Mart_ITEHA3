using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<AppUser> _userManager;

        public CustomerService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUser>> GetAllCustomers(string searchString)
        {
            var users = await _userManager.GetUsersInRoleAsync("customer");

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString) || u.LastName.Contains(searchString) || u.Email.Contains(searchString)).ToList();
            }

            return users;
        }

        public async Task<AppUser> FindById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<AppUser> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return user;
        }
    }
}
