using Agency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}