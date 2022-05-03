using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.WebApi.Models
{
    public class CredentialsViewModel
    {
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }

    }
}
