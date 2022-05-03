using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Models;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.ApiControllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<CrmUser> _userManager;

        public AuthController(
            UserManager<CrmUser> userManager
        )
        {
            _userManager = userManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
        { // TODO: create a CredentialsViewModel class in the next assignment
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            // TODO: add Authenticate method (see lesson below)
            var user = await Authenticate(credentials.EmailAddress, credentials.Password);
            if (user == null)
            {
                return UnprocessableEntity("Invalid username or password.");
            }

            // TODO: add GetUserData method (see lesson below)
            var userModel = await GetUserData(user);
            // returns a UserSummaryViewModel containing a JWT and other user info
            return Ok(userModel);
        }
        private async Task<CrmUser> Authenticate(string emailAddress, string password)
        {
            if (string.IsNullOrEmpty(emailAddress) ||
                string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<CrmUser>(null);
            }

            var user = await _userManager.FindByEmailAsync(emailAddress);

            if (user == null)
            {
                return await Task.FromResult<CrmUser>(null);
            }

            var validUser = await _userManager.CheckPasswordAsync(user, password);
            if (validUser)
            {
                return await Task.FromResult(user);
            }
            else
            {
                return await Task.FromResult<CrmUser>(null);
            }
        }

    }
}
