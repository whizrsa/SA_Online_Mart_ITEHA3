using SA_Online_Mart.Data;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetSessionKey(string userId) =>
            $"Cart_{userId}";

        public async Task AddToCart(string userId, int productId, int quantity)
        {
            var cart = await GetCart(userId);
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new ArgumentException("Invalid Product ID");
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.Product.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    CartItemId = Guid.NewGuid().GetHashCode(),
                    Product = product,
                    Quantity = quantity
                });
            }

            SaveCart(userId, cart);
        }

        public Task ClearCart(string userId)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove(GetSessionKey(userId));

            return Task.CompletedTask;
        }

        public Task<Cart> GetCart(string userId)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = SessionExtensions.GetObjectFromJson<Cart>(session, GetSessionKey(userId));
            return Task.FromResult(cart ?? new Cart { Items = new List<CartItem>() });
        }

        public async Task RemoveFromCart(string userId, int cartItemId)
        {
            var cart = await GetCart(userId);
            var cartItem = cart.Items.FirstOrDefault(i => i.CartItemId == cartItemId);

            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
            }

            SaveCart(userId, cart);
        }

        public async Task UpdateCartItem(string userId, int cartItemId, int quantity)
        {
            var cart = await GetCart(userId);
            var cartItem = cart.Items.FirstOrDefault(i => i.CartItemId == cartItemId);

            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    cart.Items.Remove(cartItem);
                }
            }

            SaveCart(userId, cart);
        }

        private void SaveCart(string userId, Cart cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            SessionExtensions.SetObjectAsJson(session, GetSessionKey(userId), cart);
        }
    }
}
