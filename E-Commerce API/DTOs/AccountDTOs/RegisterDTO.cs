using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.AccountDTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmedPassword { get; set; }


    }
}
