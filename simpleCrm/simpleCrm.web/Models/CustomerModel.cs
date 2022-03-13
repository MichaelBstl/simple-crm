using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.web.Models
{
    public class CustomerModel
    {
        [Display(Name = "First Name")]
        public int Id { get; set; }
        [Display(Name = "Last Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
