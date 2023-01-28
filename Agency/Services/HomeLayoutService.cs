using Agency.DAL;
using Agency.Models;

namespace Agency.Services
{
    public class HomeLayoutService
    {
        private readonly AppDbContext _context;

        public HomeLayoutService(AppDbContext context)
        {
            _context = context;
        }
        public List<Setting> GetSettings() 
        {
            return _context.Settings.ToList();
        }
    }
}
