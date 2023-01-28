using Agency.DAL;
using Agency.Helpers;
using Agency.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Agency.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PortfolioController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Portfolios.Include(x => x.Category).AsQueryable();
            PaginatedList<Portfolio> list = PaginatedList<Portfolio>.Create(query,page,2);
            return View(list);
        }
        public IActionResult Create()//Category-den sonra yaradilmalidir. Chunki aralarinda one to many elaqe var
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Portfolio portfolio)
        {
            ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid) return View(portfolio);
            if (portfolio.Image is null)
            {
                ModelState.AddModelError("Image", "Image field is required");
                return View(portfolio);
            }
            if (portfolio.Image.ContentType != "image/png" && portfolio.Image.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("Image", "Image format should be png or jpeg");
                return View(portfolio);
            }
            if (portfolio.Image.Length > 2097152)
            {
                ModelState.AddModelError("Image", "Image size should be less than 2mb");
                return View(portfolio);
            }
            portfolio.ImageUrl = FileManager.Save(_env.WebRootPath, "uploads/portfolio", portfolio.Image);
            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            Portfolio portfolio = _context.Portfolios.FirstOrDefault(x => x.Id == id);
            if (portfolio == null) return View("Error");
            return View(portfolio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Portfolio newPortfolio)
        {
            ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid) return View(newPortfolio);
            Portfolio oldPortfolio = _context.Portfolios.FirstOrDefault(p => p.Id == newPortfolio.Id);
            if (newPortfolio.Image != null)
            {
                if (newPortfolio.Image.ContentType != "image/png" && newPortfolio.Image.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Image", "Image format should be png or jpeg");
                    return View(newPortfolio);
                }
                if (newPortfolio.Image.Length > 2097152)
                {
                    ModelState.AddModelError("Image", "Image size should be less than 2mb");
                    return View(newPortfolio);
                }
                FileManager.Delete(_env.WebRootPath, "uploads/portfolio", oldPortfolio.ImageUrl);
                oldPortfolio.ImageUrl = FileManager.Save(_env.WebRootPath, "uploads/portfolio", newPortfolio.Image);
            }
            oldPortfolio.Title = newPortfolio.Title;
            oldPortfolio.Description = newPortfolio.Description;
            oldPortfolio.CategoryId = newPortfolio.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Portfolio portfolio = _context.Portfolios.FirstOrDefault(p => p.Id == id);
            if (portfolio == null) return View("Error");
            FileManager.Delete(_env.WebRootPath, "uploads/portfolio", portfolio.ImageUrl);
            _context.Remove(portfolio);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
