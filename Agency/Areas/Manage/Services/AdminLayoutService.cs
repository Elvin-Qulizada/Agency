using Agency.Models;
using Microsoft.AspNetCore.Identity;

namespace Agency.Areas.Manage.Services
{
    public class AdminLayoutService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public AdminLayoutService(IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<AppUser> GetUserAsync()
        {
            AppUser appUser =  await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            return appUser;
        }
    }
}
