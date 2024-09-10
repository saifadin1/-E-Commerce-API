namespace E_Commerce_API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
