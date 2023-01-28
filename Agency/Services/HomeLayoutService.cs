using Agency.DAL;
using Agency.Models;
using Microsoft.AspNetCore.Identity;

namespace Agency.Services
{
    public class HomeLayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public HomeLayoutService(AppDbContext context,IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public List<Setting> GetSettings() 
        {
            return _context.Settings.ToList();
        }
        public async Task<AppUser> GetUser()
        {
            AppUser appUser = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }
            return appUser;
        }
    }
}
