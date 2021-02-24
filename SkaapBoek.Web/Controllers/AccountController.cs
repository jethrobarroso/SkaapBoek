using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkaapBoek.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signingManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signingManager, UserManager<IdentityUser> userManager)
        {
            _signingManager = signingManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signingManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signingManager.IsSignedIn(User))
                return View("Sheep");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signingManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // Check if returnUrl is local to avoid open redirect attacks
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        // Alternatively, the following will throw exception if it is not 
                        // a local URL. 
                        //return LocalRedirect(returnUrl);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "sheep");
                    }
                }

                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
