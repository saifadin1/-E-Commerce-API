namespace E_Commerce_API.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
    }
}
