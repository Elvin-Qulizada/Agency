using Agency.Models;
using Agency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) return View(loginViewModel);
            AppUser appUser = await _userManager.FindByNameAsync(loginViewModel.Username);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(loginViewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(appUser, loginViewModel.Password,false,false);
            if (!result.Succeeded) 
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(loginViewModel);
            }
            return RedirectToAction("Index", "dashboard");
        }
        public IActionResult Logout() 
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}
