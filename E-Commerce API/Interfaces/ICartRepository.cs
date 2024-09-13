using E_Commerce_API.DTOs.CartDTOs;

namespace E_Commerce_API.Interfaces
{
    public interface ICartRepository
    {
        Task<ICollection<Product>> GetAll(string UserId);
        Task<decimal> GetPrice(string UserId);
        Task AddProduct(AddToCartDTO addToCartDTO);
        Task RemoveProduct(AddToCartDTO addToCartDTO);
    }
}
