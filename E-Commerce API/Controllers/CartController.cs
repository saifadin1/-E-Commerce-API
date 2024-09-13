using E_Commerce_API.DTOs.CartDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartRepository cartRepository;
        IProductRepository productRepository;
        UserManager<AppUser> userManager;
        public CartController(ICartRepository cartRepository , UserManager<AppUser> userManager, IProductRepository productRepository)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.productRepository = productRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDTO addToCartDTO)
        {
            var user = await userManager.FindByIdAsync(addToCartDTO.UserId);
            var product = await productRepository.GetProduct(addToCartDTO.ProductId);
            if (user == null)
                return BadRequest("User not found.");
            if (product == null)
                return BadRequest("Product not found.");
            await cartRepository.AddProduct(addToCartDTO);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if(user == null)
            {
                return BadRequest(); 
            }
            var products = await cartRepository.GetAll(UserId);
            return Ok(products);
        }


        [HttpGet("Cost")]
        public async Task<IActionResult> GetCost(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if(user == null)
            {
                return BadRequest(); 
            }
            var cost = await cartRepository.GetPrice(UserId);
            return Ok(cost);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(AddToCartDTO addToCartDTO)
        {
            var user = await userManager.FindByIdAsync(addToCartDTO.UserId);
            if(user == null)
            {
                return BadRequest(); 
            }
            await cartRepository.RemoveProduct(addToCartDTO);
            return Ok();
        }
    }
}
