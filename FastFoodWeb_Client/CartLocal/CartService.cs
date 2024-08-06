using Blazored.LocalStorage;
using System.Text.Json;
using Data.Models.CartModels;

namespace FastFoodWeb_Client.CartLocal
{
    public class CartService
    {
        private readonly ILocalStorageService _localStorage;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<CartView> GetCartAsync()
        {
            var cartJson = await _localStorage.GetItemAsStringAsync("cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                return new CartView(); // Khởi tạo một Cart mới nếu cartJson rỗng
            }
            return JsonSerializer.Deserialize<CartView>(cartJson);
        }

        public async Task AddToCartAsync(Guid productId, int quantity)
        {
            var cart = await GetCartAsync();
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
            }
            await UpdateCartAsync(cart);
        }

        public async Task RemoveFromCartAsync(Guid productId)
        {
            var cart = await GetCartAsync();
            cart.Items.RemoveAll(i => i.ProductId == productId);
            await UpdateCartAsync(cart);
        }

        public async Task UpdateCartAsync(CartView cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            await _localStorage.SetItemAsync("cart", cartJson);
        }
    }
}

