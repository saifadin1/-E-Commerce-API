using E_Commerce_API.DTOs.Category;
using E_Commerce_API.DTOs.ProductDTOs;

namespace E_Commerce_API.Services
{
    public class CategoryRepoService : ICategoryRepository
    {
        AppDbContext _context;

        public CategoryRepoService(AppDbContext context)
        {
            _context = context;  
        }

        public async Task AddCategory(AddCategoryDTO _category)
        {
            await _context.Categories.AddAsync(new Category
            {
                Name = _category.Name
            });
            _context.SaveChanges();
        }

        public async Task<bool> DeleteCategory(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if(category is Category)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ICollection<GetCategoryDTO>> GetCategories()
        {
            var categories = await _context.Categories.Select(c => new GetCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                ProductCount = c.Products.Count
            }).ToListAsync();
            return categories;
        }

        public async Task<GetCategoryDTO> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return null;
            return new GetCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                ProductCount = category.Products.Count
            };
        }

        public Task<ICollection<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
