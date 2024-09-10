using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
