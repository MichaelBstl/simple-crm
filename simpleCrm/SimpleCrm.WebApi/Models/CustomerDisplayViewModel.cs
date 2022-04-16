using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerDisplayViewModel
    {
        public CustomerDisplayViewModel(Customer customer)
        {
            CustomerId = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            PhoneNumber = customer.PhoneNumber;
            EmailAddress = customer.EmailAddress;
            PreferredContactMethod = Enum.GetName(typeof(InteractionMethod), customer.ContactMethod);
            Status = Enum.GetName(typeof(InteractionMethod), customer.Status);
            LastContactDate = customer.LastContactDate.Year > 1 ? customer.LastContactDate.ToString("s", CultureInfo.InstalledUICulture) : "";

        }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PreferredContactMethod { get; set; }
        public string Status { get; set; }
        public string LastContactDate { get; set; }
    }
}