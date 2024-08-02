using System.ComponentModel.DataAnnotations;
using Zincirimr.Data.Models;

namespace Zincirimr.Web.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? SurName { get; set; } = string.Empty;

        public string? UserId { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }
        [Required]
        public string? Street { get; set; } = string.Empty;
        [Required]
        public string? City { get; set; } = string.Empty;
        [Required]
        public string? State { get; set; } = string.Empty;
        [Required]
        public string? PostalCode { get; set; } = string.Empty;
        [Required]
        public string? Country { get; set; } = string.Empty;
        [Required]
        public string? ContactPhoneNumber { get; set; } = string.Empty;
        [Required]
        public string? FullAddress { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public AddressListViewModel AdressList { get; set; } = new AddressListViewModel();

    }

    public class AddressListViewModel
    {
        public List<AddressViewModel> Addressess { get; set; } = new List<AddressViewModel>();
    }
}
