﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.web.Models.Account;
using System.Threading.Tasks;

namespace SimpleCrm.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<CrmUser> _signInManager;
        private readonly UserManager<CrmUser> userManager;
        public AccountController(UserManager<CrmUser> userManager, SignInManager<CrmUser> signInManager)
        {  // injection is done in the "constructor" method of the class
            this.userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new CrmUser
                {
                    UserName = model.UserName,
                    DisplayName = model.DisplayName,
                    Email = model.UserName
                };
                var createResult = await this.userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
  foreach (var result in createResult.Errors)
  {
      ModelState.AddModelError("", result.Description);
  }            }
            return View();
        }
    }
}
