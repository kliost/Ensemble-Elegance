using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ensemble_Elegance.Models;
using System.Text.Json;
using Ensemble_Elegance.Services;
using System.Runtime.CompilerServices;
using System.Net;

namespace Ensemble_Elegance.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            //_context = context;
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult AddNewproduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewproduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                await _adminService.AddNewproduct(product);
            }

            return RedirectToAction("Catalogue", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Updateproduct(ProductModel newproduct)
        {
            await _adminService.Updateproduct(newproduct);
            return RedirectToAction("productList", "Admin");
        }

        [HttpGet]
        public IActionResult Updateproduct(int id)
        {
            var productToUpdate = _adminService.GetProductByIdAsync(id);
            return View(productToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Deleteproduct(id);
            return RedirectToAction("productList");
        }

        [HttpGet]
        public IActionResult AdminPage()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var products = await _adminService.GetProductListAsync();
            return View(products);
        }


        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var Users = await _adminService.GetUserListAsync();
            return View(Users);
        }


        [HttpGet]
        public async Task<IActionResult> ActiveOrders()
        {
            List<OrderModel> orders = await _adminService.GetOrderListAsync();

            return View(orders.Where(x => x.Status != OrderStatus.Received));
        }


        [HttpGet]
        public async Task<IActionResult> OrderHistory()
        {
            List<OrderModel> orders = await _adminService.GetOrderListAsync();

            return View(orders.Where(x => x.Status == OrderStatus.Received));
        }

        public async Task SetNextOrderStatus(int id)
        {
            await _adminService.SetNextOrderStatus(id);
        }

    }

}
