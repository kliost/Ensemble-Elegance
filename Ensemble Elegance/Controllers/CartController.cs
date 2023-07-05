using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ensemble_Elegance.Models;
using Ensemble_Elegance.Extensions;
using System.Text.Json;

namespace Ensemble_Elegance.Controllers
{
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ISession _session;
        public CartController(ApplicationDbContext context, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
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
        [HttpGet]
        public IActionResult AddToCartById(int itemId)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();


            ProductModel? item = _context.ShopItems.Where(x => x.Id == itemId).FirstOrDefault();

            if (item != null)
            {
                CartProductModel cartProduct = new()
                {
                    productModel = item
                };
                var ExistingProductInCart = cart.Where(x => x.productModel.Id == cartProduct.productModel.Id).FirstOrDefault();
                if (ExistingProductInCart != null)
                {
                    ExistingProductInCart.Quantity++;
                }
                else
                {
                    cartProduct.Quantity = 1;
                    cart.Add(cartProduct);
                }
            }



            _session.SetObject("Cart", cart);
            return RedirectToAction("Cart", "Cart");
        }

        public IActionResult Cart()
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();

            return View(cart);
        }

        public IActionResult IncrementQuantity(int itemId)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
            CartProductModel? cartProduct = cart.Where(x => x.productModel.Id == itemId).FirstOrDefault();

            if (cartProduct != null)
            {
                cartProduct.Quantity++;
            }
            _session.SetObject("Cart", cart);
            return RedirectToAction("Cart", "Cart");
        }
        public IActionResult DecrementQuantity(int itemId)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
            CartProductModel? cartProduct = cart.Where(x => x.productModel.Id == itemId).FirstOrDefault();

            if (cartProduct != null)
            {

                cartProduct.Quantity--;

                if (cartProduct.Quantity <= 0)
                {
                    cart.Remove(cartProduct);
                }
            }
            _session.SetObject("Cart", cart);
            return RedirectToAction("Cart", "Cart");
        }
        public IActionResult UpdateQuantity(int itemId, int newQuantity)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
            CartProductModel? cartProduct = cart.Where(x => x.productModel.Id == itemId).FirstOrDefault();
            if (cartProduct != null)
            {
                cartProduct.Quantity = newQuantity;
            }
            _session.SetObject("Cart", cart);
            return RedirectToAction("Cart", "Cart");
        }
        public IActionResult DeleteFromCart(int itemId)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
            CartProductModel? cartProduct = cart.Where(x => x.productModel.Id == itemId).FirstOrDefault();
            if (cartProduct != null) cart.Remove(cartProduct);
            return RedirectToAction("Cart", "Cart");
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            OrderModel order = new OrderModel();
            return View(order);
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderModel order)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart");
            if (cart != null)
            {
                order.Status = OrderStatus.PendingConfirmation;
                order.OrderListJson = JsonSerializer.Serialize(cart);
                _context.Orders.Add(order);
                _context.SaveChanges();
                return View("ThanksForOrder");
            }
            else
            {
                return View("Index");
            }
        }
    }
}
