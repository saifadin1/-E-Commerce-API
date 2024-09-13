
namespace E_Commerce_API.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Cart = new Cart();
        }
        public int? CartId { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orderes { get; set; }
    }
}
