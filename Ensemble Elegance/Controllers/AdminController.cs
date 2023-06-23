using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ensemble_Elegance.Models;
using System.Text.Json;

namespace Ensemble_Elegance.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ApplicationDbContext context, UserManager<UserModel> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }


        public void SaveImage(ProductModel item)
        {
            string itemIdFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", item.Id.ToString());
            string filepath = itemIdFolder + @"\" + item?.imageFile?.FileName;

            if (!Directory.Exists(itemIdFolder))
            {
                Directory.CreateDirectory(itemIdFolder);
            }

            var stream = new FileStream(filepath, FileMode.Create);

            item.imageFile.CopyTo(stream);

            stream.Close();
        }

        //CRUD

        //Create

        [HttpGet]
        public IActionResult AddNewItem()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddNewItem(ProductModel item)
        {
            if (ModelState.IsValid)
            {

                if (item.imageFile != null)
                {
                    //Saving item to DB
                    item.ImageFileName = item.imageFile.FileName;
                    item.CategoriesJson = JsonSerializer.Serialize(item.CategoriesList);

                    _context.ShopItems.Add(item);
                    await _context.SaveChangesAsync();

                    SaveImage(item);

                }
            }

            return RedirectToAction("Catalogue", "Home");
        }


        //Update

        [HttpPost]
        public IActionResult UpdateItem(ProductModel newItem)
        {
            ProductModel oldItem = _context.ShopItems.First(x => x.Id == newItem.Id);
            _context.Entry(oldItem).State = EntityState.Detached;

            // if image is changed
            if (newItem.imageFile != null)
            {
                System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "images", oldItem.Id.ToString(), oldItem.ImageFileName));

                newItem.ImageFileName = newItem.imageFile.FileName;
                SaveImage(newItem);

            }
            // if image is not changed
            else
            {
                newItem.ImageFileName = oldItem.ImageFileName;

            }

            _context.ShopItems.Update(newItem);
            _context.SaveChanges();
            return RedirectToAction("ItemList", "Admin");

        }

        [HttpGet]
        public IActionResult UpdateItem(int id)
        {
            var itemToUpdate = _context.ShopItems.First(x => x.Id == id);
            return View(itemToUpdate);
        }

        //Delete

        public IActionResult Delete(int id)
        {
            var itemToDelete = _context.ShopItems.First(x => x.Id == id);

            System.IO.Directory.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "images", itemToDelete.Id.ToString()), recursive: true);

            _context.ShopItems.Remove(itemToDelete);
            _context.SaveChanges();
            return RedirectToAction("ItemList");
        }


        //Navigation
        [HttpGet]
        public IActionResult AdminPage()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ItemList()
        {
            var itemlist = await _context.ShopItems.ToListAsync();
            return View(itemlist);
        }




        [HttpGet]
        public async Task<IActionResult> UserList()
        {


            var Users = await _context.Users.ToListAsync();
            return View(Users);
        }





    }
}
