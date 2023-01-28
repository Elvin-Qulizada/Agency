using Agency.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateUser()
        {
            AppUser user = new AppUser
            {
                Fullname = "Elvin Qulizada",
                UserName = "elvin",
                Password = "elvin123"
            };
            await _userManager.CreateAsync(user);
            await _userManager.AddPasswordAsync(user, user.Password);
            return Ok(user);
        }
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole superAdmin = new IdentityRole("SuperAdmin");
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole member = new IdentityRole("Member");
            await _roleManager.CreateAsync(superAdmin);
            await _roleManager.CreateAsync(admin);
            await _roleManager.CreateAsync(member);
            return Ok("successful");
        }
        public async Task<IActionResult> AssignRole()
        {
            AppUser appUser = await _userManager.FindByNameAsync("elvin");
            await _userManager.AddToRoleAsync(appUser, "SuperAdmin");
            return Ok("successful");
        }
    }
}
