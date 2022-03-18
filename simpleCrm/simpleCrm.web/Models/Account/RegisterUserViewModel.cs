using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.web.Models.Account
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(256)] // TODO: Set annotation attributes in the assignment
        public string UserName { get; set; }
        [Required, MaxLength(256), DisplayName("Name")]
        public string DisplayName { get; set; }
        [Required, MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MinLength(6), Compare("Password", ErrorMessage = "The Passwords do not match"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
