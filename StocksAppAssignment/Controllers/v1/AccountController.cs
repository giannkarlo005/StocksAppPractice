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
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly SignInManager<ApplicationRole> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationUser> roleManager,
                                 SignInManager<ApplicationRole> signInManager)
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
        public async Task<IActionResult> RegisterUser(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid)
            {
                IEnumerable<string> stateErrorMessages = ModelState.Values.SelectMany(x => x.Errors)
                                                                          .Select(x => x.ErrorMessage);\
                
                string errorMessage = string.Join(" | ", stateErrorMessages);
                return Problem(errorMessage);
            }

            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                PersonName = registerDTO.PersonName,
                PhoneNumber= registerDTO.PhoneNumber
            };

            IdentityResult identityResult = await _userManager.CreateAsync(applicationUser, registerDTO.Password);

            if(identityResult.Succeeded)
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
            if(applicationUser == null) 
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
