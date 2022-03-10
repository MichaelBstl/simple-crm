using Microsoft.AspNetCore.Mvc;
using SimpleCrm.web.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new CostomerModel { FirstName = "Michael", LastName = "Bryant", Id = 1, PhoneNumber = "314-443-8763" };
            return View(model);
        }
    }
}
