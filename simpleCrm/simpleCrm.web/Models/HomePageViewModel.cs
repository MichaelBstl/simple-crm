using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.web.Models
{
    public class HomePageViewModel
    {

        public string CurrentMessage { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
