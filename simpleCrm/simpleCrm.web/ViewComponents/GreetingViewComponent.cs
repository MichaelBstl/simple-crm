using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.web.ViewComponents
{
    public class GreetingViewComponent : ViewComponent
    {
        private readonly IGreeter greeter;

        public GreetingViewComponent(IGreeter greeter)
        {
            this.greeter = greeter;
        }

        public IViewComponentResult Invoke()
        {
            var model = greeter.GetGreeting();
            return View("Default", model);
        }
    }
}
