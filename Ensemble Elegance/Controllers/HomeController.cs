using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ensemble_Elegance.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<IActionResult> Catalogue(int page = 1)
        {
            int pagesize = 25;

            var items = await _context.ShopItems.ToListAsync();

            int totalItems = items.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);
            var paginatedData = items.Skip((page - 1) * pagesize).Take(pagesize).ToList();

            var paginationModel = new PaginationModel
            {
                CurrentPage = page,
                PageSize = pagesize,
                PageCount = totalPages,
            };
            ViewData["paginationModel"] = paginationModel;
            return View(paginatedData);
        }

        public IActionResult UserPage()
        {
            return View();
        }

    }
}