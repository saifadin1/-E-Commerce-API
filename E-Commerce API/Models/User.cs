using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orderes { get; set; }
    }
}
