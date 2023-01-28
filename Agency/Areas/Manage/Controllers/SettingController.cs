using Agency.DAL;
using Agency.Helpers;
using Agency.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Settings.ToList());
        }
        public IActionResult Update(int id)
        {
            Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);
            if (setting == null) return View("Error");
            return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Setting newSetting)
        {
            if(!ModelState.IsValid) return View(newSetting);
            Setting oldSetting = _context.Settings.FirstOrDefault(s=>s.Id== newSetting.Id);
            oldSetting.Value= newSetting.Value;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
