using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public interface IShopService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
