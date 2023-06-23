using Microsoft.AspNetCore.Mvc;
using Ensemble_Elegance.Models;
using Ensemble_Elegance.Extensions;


namespace Ensemble_Elegance.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly ISession _session;
        public HomeController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            List<ProductModel> visitedProducts = _session.GetObject<List<ProductModel>>("VisitedProducts");
            ViewBag.visitedProducts = visitedProducts;
            return View();
        }



        public IActionResult About() => View();
        public IActionResult Contacts() => View();


        public IActionResult Catalogue(int page = 1, int pagesize = 25)
        {

            var items = _context.ShopItems.ToList();

            var paginationModel = new PaginationModel
            {
                CurrentPage = page,
                PageSize = pagesize,
            };
            var paginateddata = paginationModel.GetPagination(items);
            ViewData["paginationModel"] = paginationModel;
            return View(paginateddata);
        }

        public IActionResult UserPage()
        {
            return View();
        }

        public IActionResult ProductPage(int productId)
        {


            try
            {
                ProductModel? product = _context.ShopItems.Where(x => x.Id == productId).FirstOrDefault();

                List<ProductModel> visitedProducts = _session.GetObject<List<ProductModel>>("VisitedProducts") ?? new List<ProductModel>();

                if (visitedProducts.Contains(product))
                {
                    visitedProducts.Remove(product);

                }
                visitedProducts.Insert(0, product);

                if (visitedProducts.Count >= 6)
                {
                    visitedProducts.RemoveAt(visitedProducts.Count - 1);
                }


                _session.SetObject("VisitedProducts", visitedProducts);

                return View(product);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

    }
}