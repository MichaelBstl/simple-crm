using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.web.Models.Account
{
    public class LoginUserViewModel
    {
        [Required, MaxLength(256)] 
        public string UserName { get; set; }
        [Required, MaxLength(256), DisplayName("Name")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}
