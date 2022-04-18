using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerUpdateViewModel
    {
        public CustomerUpdateViewModel(Customer customer)
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            PhoneNumber = customer.PhoneNumber;
            EmailAddress = customer.EmailAddress;
            PreferredContactMethod = customer.ContactMethod;
            OptInNewsletter = customer.OptInNewsletter;
            Type = customer.Type;
            Status = customer.Status;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public InteractionMethod PreferredContactMethod { get; set; }
        public bool OptInNewsletter { get; set; }
        public CustomerType Type { get; set; }
        public CustomerStatus Status { get; set; }


    }
}