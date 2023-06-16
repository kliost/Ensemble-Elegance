using System.ComponentModel.DataAnnotations;

namespace Ensemble_Elegance.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AllowExtensionAttribute : ValidationAttribute
    {
        public string Extensions { get; set; } = "png, jpg, jpeg";

        public override bool IsValid(object? value)
        {
            IFormFile? file = value as IFormFile;

            bool isValid = true;
            List<string> allowedExtensions = this.Extensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (file != null)
            {
                var fileName = file.FileName;
                isValid = allowedExtensions.Any(x => fileName.EndsWith(x));
            }


            return isValid;
        }
    }
}
