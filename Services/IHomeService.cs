using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<Product>> SortProducts();
        Task<Product> GetProductById(int? id);
    }
}
