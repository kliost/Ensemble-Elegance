using Microsoft.AspNetCore.Mvc;
using Ensemble_Elegance.Models;
using Ensemble_Elegance.Extensions;
using System.Text.Json;
using Ensemble_Elegance.Services;

namespace Ensemble_Elegance.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {

            _cartService = cartService;

        }


        [HttpGet]
        public IActionResult Cart()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }


        [HttpGet]
        public IActionResult AddToCartById(int productId)
        {
            _cartService.AddToCartById(productId);
            return RedirectToAction("Cart", "Cart");
        }


        public IActionResult UpdateQuantity(int productId, int newQuantity)
        {
            _cartService.UpdateQuantity(productId, newQuantity);
            return Json(new { success = true });
        }


        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteFromCart(productId);
            return RedirectToAction("Cart", "Cart");
        }


        [HttpGet]
        public IActionResult PushOrder()
        {
            OrderModel order = new OrderModel();
            return View(order);
        }


        [HttpPost]
        public IActionResult PushOrder(OrderModel order)
        {
            List<CartProductModel> cart = _cartService.GetCart();
            if (cart != null)
            {
                _cartService.PushOrder(order);
                return View("ThanksForOrder");
            }
            else
            {
                return View("Index");
            }
        }
    }
}
