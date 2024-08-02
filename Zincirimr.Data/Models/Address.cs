using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zincirimr.Data.Models
{
    public class Address
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
    }
}
