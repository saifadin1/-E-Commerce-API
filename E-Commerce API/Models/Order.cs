namespace E_Commerce_API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public AppUser User { get; set; }
        public ICollection<Product> Products { get; set; }
        public int Method { get; set; }
    }
}
