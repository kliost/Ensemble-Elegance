using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Ensemble_Elegance.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string OrderItemList { get; set; }
    }
}
