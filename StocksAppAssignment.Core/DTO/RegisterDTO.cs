using System.ComponentModel.DataAnnotations;

namespace StocksAppAssignment.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Email Address should have a proper format")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Person Name is Required")]
        public string PersonName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone Numbere is Required")]
        [RegularExpression("$[0-9]*^")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
