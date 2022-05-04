using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.WebApi.Models.Auth
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}