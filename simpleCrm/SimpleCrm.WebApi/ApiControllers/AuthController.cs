using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleCrm.WebApi.Auth;
using SimpleCrm.WebApi.Models;
using SimpleCrm.WebApi.Models.Auth;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.ApiControllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<CrmUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly MicrosoftAuthSettings _microsoftAuthSettings;

        public AuthController(
            UserManager<CrmUser> userManager,
            IJwtFactory jwtFactory,
            IConfiguration configuration,
            ILogger<AuthController> logger,
            IOptions<MicrosoftAuthSettings> microsoftAuthSettings
        )
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _configuration = configuration;
            _logger = logger;
            _microsoftAuthSettings = microsoftAuthSettings.Value;
        }

        [HttpGet("external/microsoft")]
        public IActionResult GetMicrosoft()
        {   // only needed for the client to know what to send to Microsoft on the front-end redirect to login with Microsoft
            return Ok(new
            {   //this is the public application id, don't return the secret 'Password' here!
                client_id = _microsoftAuthSettings.ClientId,
                scope = "https://graph.microsoft.com/user.read",
                state = "" //arbitrary state to return again for this user
            });
        }

        [HttpPost("external/microsoft")]
        public async Task<IActionResult> PostMicrosoft([FromBody] MicrosoftAuthViewModel model)
        {
            var verifier = new MicrosoftAuthVerifier<AuthController>(_microsoftAuthSettings, _configuration["HttpHost"] + (model.BaseHref ?? "/"), _logger);
            var profile = await verifier.AcquireUser(model.AccessToken);

            if (!profile.IsSuccessful)
            {
                _logger.LogWarning("ExternalLoginCallback() unknown error at external login provider, (profile.ErrorMessage)", profile.Error.Message);
                return StatusCode(StatusCodes.Status400BadRequest, profile.Error.Message);
            }
            var info = new UserLoginInfo("Microsoft", profile.Id, "Microsoft");
            if (info == null || info.ProviderKey == null || info.LoginProvider == null)
            {
                _logger.LogWarning("ExternalLoginCallback() unknown error at external login provider");
                return StatusCode(StatusCodes.Status400BadRequest, "Unknown error at external login provider");
            }
            if (string.IsNullOrWhiteSpace(profile.Mail))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Email Address not available from provider.");
            }

            var user = await _userManager.FindByEmailAsync(profile.Mail);
            if (user == null)
            {
                var appUser = new CrmUser
                {
                    DisplayName = profile.DisplayName,
                    Email = profile.Mail,
                    UserName = profile.Mail,
                    PhoneNumber = profile.MobilePhone
                };

                var password = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8) + "#1aA";
                var identityResult = await _userManager.CreateAsync(appUser, password); 
                if (!identityResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Could not create user.");
                }

                user = await _userManager.FindByEmailAsync(profile.Mail);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "faild to create local user");
                }
            }
            var userModel = await GetUserData(user);
            return Ok(userModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var user = await Authenticate(credentials.EmailAddress, credentials.Password);
            if (user == null)
            {
                return UnprocessableEntity("Invalid username or password.");
            }

            var userModel = await GetUserData(user);
            return Ok(userModel);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel userData)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var user = new CrmUser
            {
                DisplayName = userData.Name,
                Email = userData.EmailAddress,
                UserName = userData.EmailAddress
            };

            var response = _userManager.CreateAsync(user, userData.Password);

            _logger.LogInformation("User {0} Created", userData.Name);
            var identity = await Authenticate(userData.EmailAddress, userData.Password);
            var userModel = await GetUserData(identity);

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
        [Authorize(Policy = "ApiUser")]
        [HttpPost("verify")] // POST api/auth/verify
        public async Task<IActionResult> Verify()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.Claims.Single(c => c.Type == "id");
                var user = _userManager.Users.FirstOrDefault(x => x.Id.ToString() == userIdClaim.Value);
                if (user == null)
                    return Forbid();

                var userModel = await GetUserData(user);
                return new ObjectResult(userModel);
            }

            return Forbid();
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
