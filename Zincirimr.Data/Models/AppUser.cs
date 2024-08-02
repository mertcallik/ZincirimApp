using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zincirimr.Data.Models
{
    public class AppUser : IdentityUser
    {
        
        public string? FirstName { get; set; }=string.Empty;
        public string? LastName { get; set; }=string.Empty;
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<Order>? Orders { get; set; }
    }
}
