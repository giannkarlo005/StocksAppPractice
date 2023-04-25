using System.ComponentModel.DataAnnotations;

namespace StocksAppAssignment.Core.DTO
{
    public class RegisterDTO
    {
        //Email Address
        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Email Address should have a proper format")]
        public string EmailAddress { get; set; } = string.Empty;

        //Person Name
        [Required(ErrorMessage = "Person Name is Required")]
        public string PersonName { get; set; } = string.Empty;

        //Phone Number
        [Required(ErrorMessage = "Phone Number is Required")]
        [RegularExpression("^[0-9]*$")]
        public int PhoneNumber { get; set; }

        //Password
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;

        //Confirm Password
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
