using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public interface ICartService
    {
        Task<Cart> GetCart(string userId);
        Task AddToCart(string userId, int productId, int quantity);
        Task UpdateCartItem(string userId, int cartItemId, int quantity);
        Task RemoveFromCart(string userId, int cartItemId);
        Task ClearCart(string userId);
    }
}
