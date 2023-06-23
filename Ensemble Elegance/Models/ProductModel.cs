using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.Web.Mvc;
using Ensemble_Elegance.Attributes;
using System.Net;

namespace Ensemble_Elegance.Models;

public class ProductModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? ShortDescription { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public double Price { get; set; }

    [Required]
    [NotMapped]
    [AllowExtension(Extensions = "png,jpg,jpeg", ErrorMessage = "This file type is not supported")]
    public IFormFile? imageFile { get; set; }

    public string? ImageFileName { get; set; }
    [Required]
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
