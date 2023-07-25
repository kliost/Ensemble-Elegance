using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ensemble_Elegance.Attributes;


namespace Ensemble_Elegance.Models
{

    public class ProductModel
    {
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }


        [NotMapped]
        [AllowExtension(Extensions = "png,jpg,jpeg", ErrorMessage = "This file type is not supported")]
        public IFormFile? imageFile { get; set; }

        public string? ImageFileName { get; set; }

        [NotMapped]
        public List<string>? CategoriesList { get; set; }

        public string? CategoriesJson { get; set; }








        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ProductModel other = (ProductModel)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}