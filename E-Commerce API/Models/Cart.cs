namespace E_Commerce_API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }

    }
}
