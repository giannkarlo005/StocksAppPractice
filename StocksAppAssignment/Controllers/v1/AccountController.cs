using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.Identities;

namespace StocksAppAssignment.UI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }

        /// <summary>
        /// Registers New User to the site
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> stateErrorMessages = ModelState.Values.SelectMany(x => x.Errors)
                                                                          .Select(x => x.ErrorMessage);

                string errorMessage = string.Join(" | ", stateErrorMessages);
                return Problem(errorMessage);
            }

            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = registerDTO.EmailAddress,
                Email = registerDTO.EmailAddress,
                PersonName = registerDTO.PersonName,
                PhoneNumber = registerDTO.PhoneNumber.ToString()
            };

            IdentityResult identityResult = await _userManager.CreateAsync(applicationUser, registerDTO.Password);

            if (identityResult.Succeeded)
            {
                return Ok(identityResult.Succeeded);
            }

            IEnumerable<string> identityResultErrors = identityResult.Errors.Select(x => x.Description);

            string identityErrors = string.Join(" | ", identityResultErrors);
            return Problem(identityErrors);
        }

        /// <summary>
        /// Checks if the user's email address already exists in the database
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string emailAddress)
        {
            ApplicationUser? applicationUser = await _userManager.FindByEmailAsync(emailAddress);
            if (applicationUser == null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        /// <summary>
        /// Logs the User in to the site
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> stateErrorMessages = ModelState.Values.SelectMany(x => x.Errors)
                                                                          .Select(x => x.ErrorMessage);

                string errorMessage = string.Join(" | ", stateErrorMessages);
                return Problem(errorMessage);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginDTO.EmailAddress, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Problem("Invalid username and password");
            }

            return Ok(result.Succeeded);
        }

        /// <summary>
        /// Logs the user out
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
