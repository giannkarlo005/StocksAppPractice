using System.ComponentModel.DataAnnotations;

namespace StocksAppAssignment.Core.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Email Address should have a proper format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
