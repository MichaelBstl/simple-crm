using Microsoft.AspNetCore.Mvc;
using SimpleCrm.web.Models;
using SimpleCrm.web.Models.Home;
using System;
using System.Linq;

namespace SimpleCrm.web.Controllers
{
    public class HomeController : Controller
    { 
        private ICustomerData _customerData;
        private readonly IGreeter _greeter;
        public HomeController(ICustomerData customerData,
            IGreeter greeter)
        {
            _customerData = customerData;
            _greeter = greeter;
        }

        public IActionResult Details(int id)
        {
            Customer cust = _customerData.Get(id);
            if (cust == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(cust);
        }

        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    OptInNewsletter = model.OptInNewsletter,
                    Type = model.Type
                };
                _customerData.Save(customer);

                return RedirectToAction(nameof(Details), new { Id = customer.Id });
            }
            return View();

        }
        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                CurrentMessage = _greeter.GetGreeting(),
                Customers = _customerData.GetAll()
            };
            return View(model);
        }
    }
}
