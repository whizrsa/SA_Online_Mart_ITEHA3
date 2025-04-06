using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> FindId(int? id);
        Task<IEnumerable<Category>> GetCategories();
        Task<Product> Create(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(Product product);
    }
}
