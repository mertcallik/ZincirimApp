using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Zincirimr.Data.Models;
using Zincirimr.Web.Models;

namespace Zincirimr.Web.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public AppUser? AppUser { get; set; }
        public Address? Address { get; set; }
        [BindNever]
        public Cart? Cart { get; set; }

        [DataType(DataType.CreditCard)]
        [Required]
        public string? CardHolderName { get; set; }
        [DataType(DataType.CreditCard)]
        [Required]

        public string? CardNumber { get; set; }
        [DataType(DataType.CreditCard)]
        [Required]

        public string? ExpireDate { get; set; }
        [DataType(DataType.CreditCard)]
        [Required]

        public string? Cvc { get; set; }
    }
}
