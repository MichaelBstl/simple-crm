using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.web.Models.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}
