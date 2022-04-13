using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleCrm
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1),  MaxLength(30)]
        public string LastName { get; set; }
        [MinLength(7), MaxLength(12)]
        public string PhoneNumber { get; set; }
        [Display(Name = "Request News Letter")]
        public bool OptInNewsletter { get; set; }
        [Display(Name = "Customer Type")]
        public CustomerType Type { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        public InteractionMethod ContactMethod { get; set; }
        public CustomerStatus Status { get; set; }
        public DateTime LastContactDate { get; set; }
    }
}
