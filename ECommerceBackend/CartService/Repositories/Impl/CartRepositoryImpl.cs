using CartService.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CartService.Repositories.Impl
{
    public class CartRepositoryImpl : ICartRepository
    {
        private readonly IDistributedCache _redisCache;
        public CartRepositoryImpl(IDistributedCache redisCache) 
        {
            _redisCache = redisCache;
        }

        public async Task<bool> DeleteCartAsync(string userId)
        {
            await _redisCache.RemoveAsync(userId);
            return true;
        }

        public async Task<List<CartItemModel>> GetCartItemAsync(string userId)
        {
            var cartJson = await _redisCache.GetStringAsync(userId); 
            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItemModel>();

            var cart = JsonConvert.DeserializeObject<CartModel>(cartJson);
            return cart?.Items ?? new List<CartItemModel>();
        }

        public async Task<CartModel> GetCartAsync(string userId)
        {
            var cartData = await _redisCache.GetStringAsync(userId);
            return string.IsNullOrEmpty(cartData) ? null : JsonConvert.DeserializeObject<CartModel>(cartData);
        }

        public async Task<bool> UpdateCartAsync(CartModel model)
        {
            var serializedCart = JsonConvert.SerializeObject(model);
            await _redisCache.SetStringAsync(model.UserId, serializedCart);
            return true;
        }
    }
}
