
using E_Commerce_API.DTOs.CartDTOs;
using System.Runtime.InteropServices;

namespace E_Commerce_API.Services
{
    public class CartRepoService : ICartRepository
    {

        AppDbContext context;

        public CartRepoService(AppDbContext context)
        {
            this.context = context;    
        }
        public async Task AddProduct(AddToCartDTO addToCartDTO)
        {
            var cart = await context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == addToCartDTO.UserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = addToCartDTO.UserId,
                    CartProducts = new List<CartProduct>(),
                };
                await context.Carts.AddAsync(cart);
                await context.SaveChangesAsync();
            }
            CartProduct cp = context.CartProducts.Find(addToCartDTO.ProductId, cart.Id);
            if (cp == null)
            {
                cp = new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = addToCartDTO.ProductId,
                    Quantity = 1,
                };
                await context.CartProducts.AddAsync(cp);
                await context.SaveChangesAsync();
            }
            else
            {
                cp.Quantity++;
            }
            await context.SaveChangesAsync();
        }

        public async Task<ICollection<Product>> GetAll(string UserId)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == UserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = UserId,
                    CartProducts = new List<CartProduct>(),
                };
            }
            return context.CartProducts
                .Where(cp => cp.CartId == cart.Id)
                .Select(cp => cp.Product)
                .ToList();
        }

        public async Task<decimal> GetPrice(string UserId)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == UserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = UserId,
                    CartProducts = new List<CartProduct>(),
                };
            }
            return context.CartProducts
                .Where(cp => cp.CartId == cart.Id)
                .Select(cp => cp.Product.Price * cp.Quantity)
                .Sum();
        }

        public async Task RemoveProduct(AddToCartDTO addToCartDTO)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == addToCartDTO.UserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = addToCartDTO.UserId,
                    CartProducts = new List<CartProduct>(),
                };
            }
            
            var cp = context.CartProducts.Find(addToCartDTO.ProductId, cart.Id);
            if (cp == null)
            {
                return;
            }
            if (cp.Quantity > 1)
            {
                cp.Quantity--;
            }
            else
            {
                context.CartProducts.Remove(cp);
            }
            await context.SaveChangesAsync();
        }
    }
}
