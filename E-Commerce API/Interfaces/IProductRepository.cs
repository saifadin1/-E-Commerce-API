namespace E_Commerce_API.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProduct(int id);
        Task AddProduct(ProductDTO product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
