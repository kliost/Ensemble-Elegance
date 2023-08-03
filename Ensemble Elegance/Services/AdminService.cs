using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ensemble_Elegance.Models;
using System.Text.Json;

namespace Ensemble_Elegance.Services
{
    public interface IAdminService
    {
        void SaveImage(ProductModel product);
        Task AddNewproduct(ProductModel product);
        Task Updateproduct(ProductModel product);
        Task Deleteproduct(int id);
        Task SetNextOrderStatus(int Id);
        Task<ProductModel> GetProductByIdAsync(int Id);
        Task<List<ProductModel>> GetProductListAsync();
        Task<List<UserModel>> GetUserListAsync();
        Task<List<OrderModel>> GetOrderListAsync();
    }



    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminService(ApplicationDbContext context, UserManager<UserModel> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddNewproduct(ProductModel product)
        {

            if (product.imageFile != null)
            {
                //Saving product to DB
                product.ImageFileName = product.imageFile.FileName;
                product.CategoriesJson = JsonSerializer.Serialize(product.CategoriesList);

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                SaveImage(product);
            }
        }

        public async Task Deleteproduct(int id)
        {
            var productToDelete = _context.Products.First(x => x.Id == id);

            System.IO.Directory.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "images", productToDelete.Id.ToString()), recursive: true);

            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderModel>> GetOrderListAsync()
        {
            var result = await _context.Orders.ToListAsync();
            return result;
        }

        public async Task<ProductModel> GetProductByIdAsync(int Id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(product => product.Id == Id);
            return result;
        }

        public async Task<List<ProductModel>> GetProductListAsync()
        {
            var result = await _context.Products.ToListAsync();
            return result;
        }

        public async Task<List<UserModel>> GetUserListAsync()
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }

        public void SaveImage(ProductModel product)
        {
            string productIdFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", product.Id.ToString());
            string filepath = Path.Combine(productIdFolder, product?.imageFile?.FileName);

            if (!Directory.Exists(productIdFolder))
            {
                Directory.CreateDirectory(productIdFolder);
            }

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                product.imageFile.CopyTo(stream);
            }
        }

        public async Task SetNextOrderStatus(int Id)
        {
            var orderFromDb = _context.Orders.Where(x => x.Id == Id).FirstOrDefault();
            if ((int)orderFromDb.Status != Enum.GetNames(typeof(OrderStatus)).Length)
            {
                int nextStatus = (int)orderFromDb.Status + 1;
                orderFromDb.Status = (OrderStatus)nextStatus;
                _context.Orders.Update(orderFromDb);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Updateproduct(ProductModel newproduct)
        {
            ProductModel oldproduct = _context.Products.First(x => x.Id == newproduct.Id);
            _context.Entry(oldproduct).State = EntityState.Detached;

            // if image is changed
            if (newproduct.imageFile != null)
            {
                File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "images", oldproduct.Id.ToString(), oldproduct.ImageFileName));

                newproduct.ImageFileName = newproduct.imageFile.FileName;
                SaveImage(newproduct);
            }
            // if image is not changed
            else
            {
                newproduct.ImageFileName = oldproduct.ImageFileName;
            }

            _context.Products.Update(newproduct);
            await _context.SaveChangesAsync();
        }
    }
}
