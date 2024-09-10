namespace E_Commerce_API.Services
{
    public class ProductRepoService : IProductRepository
    {
        public AppDbContext context { get;}

        public ProductRepoService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<ProductDTO>> GetProducts()
        {
            var products = await context.Products.Include(p => p.Category).ToListAsync();
            var productsDTO = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                CategoryId = p.Category.Id,
                Description = p.Description
            }).ToList();
            return productsDTO;
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            var product = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
                CategoryId = product.Category.Id,
                Description = product.Description
            };
            return productDTO;
        }

        public async Task AddProduct(ProductDTO _product)
        {
            var product = new Product
            {
                Name = _product.Name,
                Price = _product.Price,
                CategoryId = _product.CategoryId,
                Description = _product.Description
            };
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == _product.CategoryId);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
