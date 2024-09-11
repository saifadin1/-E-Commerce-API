using E_Commerce_API.DTOs.Category;
using E_Commerce_API.DTOs.ProductDTOs;

namespace E_Commerce_API.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ICollection<GetCategoryDTO>> GetCategories();
        Task<ICollection<ProductDTO>> GetProductsByCategory(int categoryId);
        Task<GetCategoryDTO> GetCategory(int id);
        Task AddCategory(AddCategoryDTO _category);
        Task<bool> DeleteCategory(int id);

    }
}
