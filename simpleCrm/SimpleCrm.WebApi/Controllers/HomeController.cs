using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCrm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("")]
        [ResponseCache(Duration = 31, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
//            throw new ApiException("An exceptional test. :)");
            return View();
        }
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            ViewData["Message"] = "Your application privacy page.";

            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [Route("firstpage")]
        [ResponseCache(Duration = 31, Location = ResponseCacheLocation.Client)]
        public IActionResult FirstPage()
        {
            ViewData["Message"] = "This is my first marketing page.";
            return View();
        }
        [Route("secondpage")]
        public IActionResult SecondPage()
        {
            ViewData["Message"] = "This is my second marketing page.";
            return View();
        }
        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
