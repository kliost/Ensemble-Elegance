using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ensemble_Elegance.Models;
using Ensemble_Elegance.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using Microsoft.AspNetCore.Razor.Hosting;

namespace Ensemble_Elegance.Controllers
{
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly ISession _session;
        public CartController(ApplicationDbContext context, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _session = httpContextAccessor.HttpContext.Session;
        }

        [HttpPost]
        public async Task PlaceOrder(OrderModel order)
        {
            if (order != null)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
            }
        }

        public IActionResult AddToCartById(int itemId)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
            ProductModel item = _context.ShopItems.Where(x => x.Id == itemId).FirstOrDefault();
            CartProductModel cartProduct =
            new() { Name = item.Name, Price = item.Price, Category = item.Category, Description = item.Description, Id = item.Id, Quantity = 1 };
            var ExistingProductInCart = cart.Where(x => x.Id == cartProduct.Id).FirstOrDefault();
            if (ExistingProductInCart != null)
            {
                ExistingProductInCart.Quantity++;
            }
            else
            {
                cart.Add(cartProduct);
            }

            _session.SetObject("Cart", cart);
            return View();
        }

        public IActionResult Cart()
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
            return View(cart);
        }


    }
}
