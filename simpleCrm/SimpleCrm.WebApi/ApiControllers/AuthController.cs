using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Auth;
using SimpleCrm.WebApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.ApiControllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<CrmUser> _userManager;
        private readonly IJwtFactory _jwtFactory;

        public AuthController(
            UserManager<CrmUser> userManager,
            IJwtFactory jwtFactory
        )
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
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

            var userModel = await GetUserData(user);
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
        private async Task<UserSummaryViewModel> GetUserData(CrmUser user)
        {
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user); 
            if (roles.Count == 0)
            {
                roles.Add("prospect");
            }

            // generate the jwt for the local user...
            var jwt = await _jwtFactory.GenerateEncodedToken(user.UserName,
                _jwtFactory.GenerateClaimsIdentity(user.UserName, user.Id.ToString()));
            var userModel = new UserSummaryViewModel
            {   //JWT could inject all these properties instaed of creating a model,
                //  but a model is a little easier to access from the client code without 
                //  decoding the token.  When this user model starts to contain arrays
                //  of complex data, including it all in the JWT value can get complicated.
                Id = user.Id,
                Name = user.DisplayName,
                EmailAddress = user.Email,
                JwtToken = jwt,
                Roles = roles.ToArray(),
                AccountId = 0
            };
            return userModel;
        }
    }
}
