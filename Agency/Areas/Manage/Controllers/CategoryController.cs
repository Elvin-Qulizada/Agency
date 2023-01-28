using Agency.DAL;
using Agency.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return View("Error");
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category newCategory)
        {
            if (!ModelState.IsValid) return View(newCategory);
            Category oldCategory = _context.Categories.FirstOrDefault(x => x.Id == newCategory.Id);
            oldCategory.Name = newCategory.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return View("Error");
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
