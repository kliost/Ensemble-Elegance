using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Ensemble_Elegance.Controllers
{
    public class CategoriesController : Controller
    {
        private List<string> categories = new List<string>
        {
            "Category 1",
            "Category 2",
            "Category 3",
            // Додайте ваші категорії тут
        };

        [HttpGet]
        public JsonResult Search(string input)
        {
            var similarCategories = categories
                .Where(c => c.ToLower().Contains(input.ToLower()))
                .ToList();

            return Json(similarCategories);
        }
    }
}
