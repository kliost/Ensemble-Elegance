using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Ensemble_Elegance.Models
{
    public enum OrderStatus
    {
        [Display(Name = "Очікується підтвердження")]
        PendingConfirmation,

        [Display(Name = "Підтверджено")]
        Confirmed,

        [Display(Name = "Відправлено")]
        Shipped,

        [Display(Name = "Отримано")]
        Received
    }
    public class OrderModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerSurname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        [Required]
        public string CustomerAddress { get; set; }

        [Required]
        public string? OrderListJson { get; set; }
        [Required]
        public OrderStatus? Status { get; set; }

    }
}
