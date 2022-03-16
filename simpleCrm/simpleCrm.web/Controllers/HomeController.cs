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
        public HomeController(ICustomerData customerData,
            IGreeter greeter)
        {
            _customerData = customerData;
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
        public IActionResult Edit(int id)
        {
            var customerData = _customerData.Get(id);
            var model = new CustomerEditViewModel
            {
                Id = id,
                FirstName = customerData.FirstName,
                LastName = customerData.LastName,
                PhoneNumber = customerData.PhoneNumber,
                OptInNewsletter = customerData.OptInNewsletter,
                Type = customerData.Type
            };
            return View(model);
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
                _customerData.Add(customer);

                return RedirectToAction(nameof(Details), new { Id = customer.Id });
            }
            return View();

        }
        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Customers = _customerData.GetAll()
            };
            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public IActionResult Edit(CustomerEditViewModel model)
        {
            var customerData = _customerData.Get(model.Id);
            if (ModelState.IsValid)
            {

                customerData.FirstName = model.FirstName;
                customerData.LastName = model.LastName;
                customerData.PhoneNumber = model.PhoneNumber;
                customerData.OptInNewsletter = model.OptInNewsletter;
                customerData.Type = model.Type;
                _customerData.Update();
                return RedirectToAction(nameof(Details), new { id = customerData.Id });
            }

            return View();
        }
    }
}
