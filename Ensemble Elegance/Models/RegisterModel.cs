using System.ComponentModel.DataAnnotations;

namespace Ensemble_Elegance.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string userName { get; set; }

  
        [Display(Name = "Surname")]
        [DataType(DataType.Text)]
        public string? userSurname { get; set; }

 
        [Display(Name = "Third name")]
        [DataType(DataType.Text)]
        public string? userThirdName { get; set; }



        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? phoneNumber { get; set; }


        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [Compare("password", ErrorMessage = "Passwords are different")]
        [DataType(DataType.Password)]
        [Display(Name = "Password confirm")]
        public string passwordConfirm { get; set; }
    }
}
