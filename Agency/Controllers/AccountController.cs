using Agency.Models;
using Agency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
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
            if (!ModelState.IsValid) return View(loginViewModel);
            AppUser appUser = await _userManager.FindByNameAsync(loginViewModel.Username);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(loginViewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(appUser, loginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(loginViewModel);
            }
            return RedirectToAction("Index", "home");
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            AppUser appUser = await _userManager.FindByNameAsync(registerViewModel.Username);
            if(appUser != null)
            {
                ModelState.AddModelError("Username", "Username already exist");
                return View(registerViewModel);
            }
            appUser = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (appUser != null)
            {
                ModelState.AddModelError("Email", "Email already in use");
                return View(registerViewModel);
            }
            appUser = new AppUser
            {
                Fullname = registerViewModel.Fullname,
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
            };
            await _userManager.CreateAsync(appUser);
            var result =  await _userManager.AddPasswordAsync(appUser, registerViewModel.Password);
            if(!result.Succeeded) 
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(registerViewModel);
                }
            }
            await _signInManager.SignInAsync(appUser, isPersistent: false);
            return RedirectToAction("Index","Home");
        }
    }
}
