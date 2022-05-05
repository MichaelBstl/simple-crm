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
            PreferredContactMethod = customer.ContactMethod;  //Enum.GetName(typeof(InteractionMethod), customer.ContactMethod);
            Status = customer.Status; //Enum.GetName(typeof(InteractionMethod), customer.Status);
            LastContactDate = customer.LastContactDate; //customer.LastContactDate.Year > 1 ? customer.LastContactDate.ToString("s", CultureInfo.InstalledUICulture) : "";

        }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public InteractionMethod PreferredContactMethod { get; set; }
        public CustomerStatus Status { get; set; }
        public DateTime LastContactDate { get; set; }
    }
}