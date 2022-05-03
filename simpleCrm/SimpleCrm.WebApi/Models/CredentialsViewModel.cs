using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.WebApi.Models
{
    public class CredentialsViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
