

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoryRepository.GetCategories();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDTO category)
        {
            if(category == null)
            {
                return BadRequest();
            }
            categoryRepository.AddCategory(category);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDTO>> GetCategory(int id)
        {
            var category = await categoryRepository.GetCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsFound = await categoryRepository.DeleteCategory(id);
            if(IsFound)
            {
                return NoContent();
            }
            return NotFound();
        }
       
    }
}

