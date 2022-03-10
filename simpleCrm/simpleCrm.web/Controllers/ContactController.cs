using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.web.Controllers
{
    [Route("contact")]
    public class ContactController
    {
        [Route("phone")]
        public string Phone()
        {
            return "999-999-9999";
        }
        [Route("name")]
        public string Name()
        {
            return "Michael";
        }
        public string Blank()
        {
            return "Add name or phone to this url";
        }
    }
}
