using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerCreateViewModel
    {
        public CustomerCreateViewModel(Customer customer)
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            PhoneNumber = customer.PhoneNumber;
            EmailAddress = customer.EmailAddress;
            PreferredContactMethod = customer.ContactMethod;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public InteractionMethod PreferredContactMethod { get; set; }
    }
}