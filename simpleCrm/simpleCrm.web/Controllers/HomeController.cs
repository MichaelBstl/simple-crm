using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.web.Controllers
{
    public class HomeController
    {
        [Description("This is a property")]
        public int MyProperty { get; set; }
        public string Index(string id)
        {
            return "Hello from a controller " + id;
        }
    }
}
