using System.ComponentModel.DataAnnotations;

namespace Ensemble_Elegance.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Login or Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}
