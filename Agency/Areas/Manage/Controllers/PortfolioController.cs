﻿using Agency.DAL;
using Agency.Helpers;
using Agency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agency.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PortfolioController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Portfolios.Include(x=>x.Category).ToList());
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Portfolio portfolio)
        {
            if (!ModelState.IsValid) return View(portfolio);
            if(portfolio.Image is null)
            {
                ModelState.AddModelError("Image", "Image field is required");
                return View(portfolio);
            }
            if(portfolio.Image.ContentType != "image/png" && portfolio.Image.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("Image", "Image format should be png or jpeg");
                return View(portfolio);
            }
            if (portfolio.Image.Length > 2097152 )
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
            Portfolio portfolio = _context.Portfolios.FirstOrDefault(x => x.Id == id);
            if (portfolio == null) return View("Error");
            return View(portfolio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Portfolio newPortfolio) 
        {
            if (!ModelState.IsValid) return View(newPortfolio);
            Portfolio oldPortfolio = _context.Portfolios.FirstOrDefault(p=>p.Id== newPortfolio.Id);
            if(newPortfolio.Image != null)
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
            oldPortfolio.CategoryId= newPortfolio.CategoryId;
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Portfolio portfolio = _context.Portfolios.FirstOrDefault(p => p.Id == id);
            if(portfolio == null) return View("Error");
            FileManager.Delete(_env.WebRootPath, "uploads/portfolio", portfolio.ImageUrl);
            _context.Remove(portfolio);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
