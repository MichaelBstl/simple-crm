using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.Web.Models.Home
{
    public class CustomerEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        [MinLength(7)]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
        [Display(Name = "Request News Letter")]
        public bool OptInNewsletter { get; set; }
        [Display(Name = "Customer Type")]
        public CustomerType Type { get; set; }
    }
}