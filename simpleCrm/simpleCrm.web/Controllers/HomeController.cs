  using Microsoft.AspNetCore.Mvc;
  using SimpleCrm.Web.Models;

namespace SimpleCrm.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
             var model = new CostomerModel { FirstName = "Michael", LastName = "Bryant", Id = 1, PhoneNumber = "314-443-8763" };
              return new ObjectResult(model);        }
    }
}
