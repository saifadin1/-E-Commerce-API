using E_Commerce_API.Models;

namespace E_Commerce_API.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProduct(int id);
        Task AddProduct(AddProductDTO product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
