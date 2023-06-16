using Ensemble_Elegance.Models;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ensemble_Elegance.TagHelpers
{
    [HtmlTargetElement("product-card")]
    public class ProductTagHelper : TagHelper
    {
        public ProductModel Product { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "product-card");

            var content =
                $@"
              <input type='hidden' value={Product.Id} />
              <img src='/images/{Product.Id}/{Product.ImageFileName}' alt='Product Image' class='product-image'>
              <h3 class='product-title'>{Product.Name}</h3>
              <p class='product-description'>{Product.ShortDescription}</p>
              <p class='product-price'>₴{Product.Price}</p>
              <a href='/Cart/AddToCartById?itemId={Product.Id}' class='add-to-cart-btn' >Додати до корзини</a>
                ";
            output.Content.SetHtmlContent(content);
        }
    }
}
