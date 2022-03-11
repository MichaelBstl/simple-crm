using Microsoft.AspNetCore.Mvc;
using SimpleCrm.web.Models;

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
