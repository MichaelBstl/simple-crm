using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.web.Models.Account
{
    public class LoginUserViewModel
    {
        [Required, MaxLength(256), DisplayName("Email Address")] 
        public string UserName { get; set; }
        [Required, MaxLength(256), DisplayName("Password"), DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}
