using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ensemble_Elegance.Models;

namespace Ensemble_Elegance.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Catalogue()
        {
            var items = await _context.ShopItems.ToListAsync();
            return View(items);
        }

        public IActionResult UserPage()
        {
            return View();
        }

    }
}