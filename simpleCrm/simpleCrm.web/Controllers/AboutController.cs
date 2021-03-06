using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.web.Controllers
{
    [Route("about")]
    public class AboutController
    {
        [Route("phone")]
        public string Phone()
        {
            return "A99-999-9999";
        }
        [Route("name")]
        public string Name()
        {
            return "MichaelA";
        }
        [Route("address")]
        public string Address()
        {
            return "4575 Cromwell Drive";
        }
        public string Blank()
        {
            return "Add name or phone to this url(aboutController)";
        }
    }
}
