using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<AppUser>> GetAllCustomersAsync();
    }
}
