﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerUpdateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1), MaxLength(50)]
        public string LastName { get; set; }
        [MinLength(7), MaxLength(12)]
        public string PhoneNumber { get; set; }
        [MaxLength(400)]
        public string EmailAddress { get; set; }
        public InteractionMethod PreferredContactMethod { get; set; }
        public bool OptInNewsletter { get; set; }
        public CustomerType Type { get; set; }
        public CustomerStatus Status { get; set; }


    }
}