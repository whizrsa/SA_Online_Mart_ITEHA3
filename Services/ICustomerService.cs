using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<AppUser>> GetAllCustomers(string searchString);
        Task<AppUser> FindById(string id);
        Task<AppUser> Delete(string id);
    }
}
