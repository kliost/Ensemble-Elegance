

using System.ComponentModel.DataAnnotations;

namespace Ensemble_Elegance.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? CategoryName { get; set; }

    }
}
