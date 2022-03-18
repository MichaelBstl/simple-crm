using Microsoft.AspNetCore.Mvc;
using SimpleCrm.web.Models.Account;


namespace SimpleCrm.web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new RegisterUserViewModel
                {
                    UserName = model.UserName,
                    DisplayName = model.DisplayName,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                };

                return View();
            }
            return View();
        }
    }
}
